using System.Collections.Generic;
using System.Linq;
using CardLibrary;
using Interfaces;

namespace PlayerLibrary
{
    /// <summary>
    /// Abstract class for standard behavior of a player
    /// </summary>
    public abstract class BasePlayer : IBasePlayer
    {
        public CardExchangeAnnouncement CardExchangeAnnouncement { get; set; }
        public Dictionary<string, List<Card>> SwappedCards { get; set; } = new Dictionary<string, List<Card>>();
        public List<Card> OnTheTable { get; set; } = new List<Card>();

        // This property is in the interface IBasePlayer to be possible to interchange names with the viewmodel
        public string PlayerName => GetType().Name;

        public PlayerTypes PlayerType { get; protected set; }

        /// <summary>
        /// CurrentDeck could either be injected by the constructor or by it's property
        /// Property injection is also a kind of dependency injection since the owner of the player decides what object to use
        /// Once a CardController is assigned the players hand will be filled with 7 cards
        /// If needed perhaps it could be changed based on number of opponents (2-3 players = 7 cards, >3 players = 5 cards)
        /// </summary>
        public CardController CurrentDeck
        {
            get =>
                // returns the value of the private field currentDeck
                currentDeck;
            set
            {
                // value is the assigned value to the property, the private field currentDeck will be set to this
                currentDeck = value;
                // since we just got a deck of cards lets pull a hand from it
                Hand = new List<Card>();
                Hand.AddRange(currentDeck.PullMany(7));
            }
        }
        // private field for the property CurrentDeck
        private CardController currentDeck;

        /// <summary>
        /// The players hand, initially created when the player is assigned a deck
        /// </summary>
        public List<Card> Hand { get; set; }

        /// <summary>
        /// The players opponents. To know who the player could ask for cards
        /// </summary>
        public IEnumerable<IBasePlayer> Opponents { get; set; }

        // Callbacks to the viewmodel to interact with console/wpf etc
        public SelectOpponentDelegate SelectOpponentsCallback { get; set; }
        public SelectCardValueDelegate SelectCardValueCallback { get; set; }
        public ShowMessageDelegate ShowMessageCallback { get; set; }
        /// <summary>
        /// Empty constructor needs to exist to be able to create an object without injecting a deck
        /// </summary>
        public BasePlayer()
        { }

        /// <summary>
        /// Sets the players deck when called upon. See further in <see cref="CurrentDeck"/>
        /// </summary>
        /// <param name="currentDeck">
        /// The deck that the CurrentDeck is set as.
        /// </param>
        public BasePlayer(CardController currentDeck) =>
            // This is dependency injection, the creator of the player also give a deck of cards that the player should use
            CurrentDeck = currentDeck;

        //Abstractions for things that should be strategic different for each player
        //Selection of an opponent and selection of a card value to ask for
        public abstract IBasePlayer SelectOpponent();
        public abstract Values SelectValueToAskFor();

        /// <summary>
        /// Concrete method for Play
        /// Each player should decide which opponent to ask for a card
        /// If no card is given player calls CardController.PullOne
        /// If PullOne returns null there are no cards left in the deck.
        /// If method returns false then player failed to get a card from opponent or deck
        /// </summary>
        public bool Play()
        {
            //Strategic selection of opponent
            IBasePlayer opponent = SelectOpponent();
            //Strategic selection of card value
            Values valueToAskFor = SelectValueToAskFor();

            ShowMessageCallback?.Invoke($"{PlayerName} asks {opponent.PlayerName} for a {valueToAskFor}");
            IEnumerable<Card> recieved = opponent.GetCards(valueToAskFor);

            // Announce the swap of cards, this callback will go back to Game Controller and announced out to all players
            //First parameter is the reciever, 
            //second parameter the sender, 
            //third parameter the card value exchanged 
            //and the fourth parameter is which cards actually got handed over from sender to reciever
            CardExchangeAnnouncement?.Invoke(this, opponent, valueToAskFor, recieved);

            // Test to see if any cards was recieved
            if (recieved.Count() == 0)
            {
                // Otherwise pull a card from the deck
                if (CurrentDeck.CardsLeft == 0)
                {
                    // No cards left in the deck, you're out!
                    return false;
                }
                Hand.Add(CurrentDeck.PullOne());
                // Sorting the hand based on cards Value.
                Hand.Sort((x, y) => x.Value.CompareTo(y.Value));

                ShowMessageCallback?.Invoke($"{PlayerName} didn't recieve any {valueToAskFor} from player {opponent.PlayerName}, pulled one from deck instead, {CurrentDeck.CardsLeft} left in deck\n");
            }
            else
            {
                ShowMessageCallback?.Invoke($"{PlayerName} got {recieved.Count()} {valueToAskFor} from {opponent.PlayerName}\n");
            }

            return true;
        }

        /// <summary>
        /// Called from a player against an opponent to get cards of a specific value that the opponent owns
        /// </summary>
        /// <param name="value">Card value asked for</param>
        /// <returns>IEnumerable of cards from players hand that matches the value asked for</returns>
        public IEnumerable<Card> GetCards(Values value)
        {
            // Linq where is used to find any card that is equal to value, it's a simplified if.
            // card => card.Value == value is known as lambda expression.
            if (Hand.Count == 0)
            {
                return null;
            }
            IEnumerable<Card> matchingCards = Hand.Where(c => c.Value == value);
            // Convert the IEnumerable to an array, just to detach the cards in it from the Hand
            // Otherwise it's not possible to do the remove part in OtherPlayersPlayed
            return matchingCards.ToArray();
        }

        /// <summary>
        /// This method could be used as a callback since it follows the definition
        /// for the delegate CardExchangeAnnouncement. 
        /// It is also called by ConsoleGameController.AnnotatePlayersOfCardExchange
        /// </summary>
        /// <param name="cardReciever"></param>
        /// <param name="cardSender"></param>
        /// <param name="cardValue"></param>
        /// <param name="cardsReceived"></param>
        public void OtherPlayersPlayed(IBasePlayer cardReciever, IBasePlayer cardSender, Values cardValue, IEnumerable<Card> cardsReceived)
        {
            // Take care of the hands for the two players
            UpdateHands(cardReciever, cardSender, cardsReceived);

            // Take care of the dictionary of known swapped cards for all players
            UpdateSwappedCardsDictionary(cardReciever.PlayerName, cardSender.PlayerName, cardsReceived);
        }

        private void UpdateHands(IBasePlayer cardReciever, IBasePlayer cardSender, IEnumerable<Card> cardsReceived)
        {
            // Take care of the hand if this is the requesting player
            if (cardReciever == this)
            {
                HandleReciever(cardsReceived);
            }

            // Take care of the hand if this is the responding player
            if (cardSender == this)
            {
                HandleSender(cardsReceived);
            }
            //Thread.Sleep(2000);
        }

        private void HandleSender(IEnumerable<Card> cardsReceived)
        {
            string saying = $"{PlayerName} says: \"I have given away ";
            if (cardsReceived.Count() > 0)
            {
                saying += CreateStringOfCards(cardsReceived);
            }
            else
            {
                saying += "none";
            }
            saying += $"\"\n";
            RemoveFromHand(cardsReceived);


            saying += $"My hand right now is ";
            saying += CreateStringOfCards(Hand);
            saying += $"\"\n";
            ShowMessageCallback?.Invoke(saying);

        }

        private void HandleReciever(IEnumerable<Card> cardsReceived)
        {
            //TODO! cardsRecieved could be null at the end
            Hand.AddRange(cardsReceived);

            // Sort the hand based on the Value
            Hand.Sort((x, y) => x.Value.CompareTo(y.Value));

            // Create a string for status update
            string saying = $"{PlayerName} says: \"I have received ";
            if (cardsReceived.Count() > 0)
            {
                saying += CreateStringOfCards(cardsReceived);
            }
            else
            {
                saying += "none";
            }
            saying += $"\"\n";
            saying += $"My hand right now is ";

            saying += CreateStringOfCards(Hand);

            saying += $"\"\n";
            ShowMessageCallback?.Invoke(saying);

            Dictionary<Values, IEnumerable<Card>> quads = Hand
                .GroupBy(key => key.Value, source => source, (key, cards) => new { Key = key, Value = cards })
                .Where(group => group.Value.Count() == 4)
                .ToDictionary(g => g.Key, g => g.Value);

            if (quads != null && quads.Count() > 0)
            {
                foreach (KeyValuePair<Values, IEnumerable<Card>> singleQuad in quads)
                {
                    saying = $"{PlayerName} got four cards of {singleQuad.Key}: ";
                    saying += CreateStringOfCards(singleQuad.Value);
                    OnTheTable.AddRange(singleQuad.Value);
                    Hand.RemoveAll(c => c.Value == singleQuad.Key);
                    saying += $"\n";
                    ShowMessageCallback?.Invoke(saying);
                }
            }
        }

        private void RemoveFromHand(IEnumerable<Card> cards)
        {
            foreach (Card card in cards)
            {
                Hand.RemoveAll(c => c.Suit == card.Suit && c.Value == card.Value);
            }
        }

        private string CreateStringOfCards(IEnumerable<Card> cards)
        {
            if (cards.Count() == 0)
            {
                return "none";
            }

            return string.Join(", ", cards);
        }

        private void UpdateSwappedCardsDictionary(string recieverName, string senderName, IEnumerable<Card> cardsReceived)
        {
            // First make sure the recieverName exists in the dictionary
            if (SwappedCards.ContainsKey(recieverName) == false)
            {
                // Create a list for known cards for recieverName
                SwappedCards.Add(recieverName, new List<Card>());
            }

            // Then make sure the senderName exists in the dictionary
            if (SwappedCards.ContainsKey(senderName) == false)
            {
                // Create a list for known cards for senderName
                SwappedCards.Add(senderName, new List<Card>());
            }

            // Assign the known cards to recieverName
            SwappedCards[recieverName].AddRange(cardsReceived);

            // Remove the known cards for senderName
            foreach (Card card in cardsReceived)
            {
                Card remove = SwappedCards[senderName].Where(c => c.Suit == card.Suit && c.Value == card.Value).FirstOrDefault();
                if (remove != null)
                {
                    SwappedCards[senderName].Remove(remove);
                }
            }
        }

    }
}
