using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerLibrary
{
    /// <summary>
    /// Abstract class for standard behavior of a player
    /// </summary>
    public abstract class BasePlayer
    {
        // HACK! to test card swapping
        public Card knownCard;

        public CardExchangeAnnouncement CardExchangeAnnouncement { get; set; }
        public Dictionary<string, List<Card>> SwappedCards { get; set; } = new Dictionary<string, List<Card>>();

        /// <summary>
        /// CurrentDeck could either be injected by the constructor or by it's property
        /// Property injection is also a kind of dependency injection since the owner of the player decides what object to use
        /// Once a CardController is assigned the players hand will be filled with 7 cards
        /// If needed it could be changed based on number of opponents (2-3 players = 7 cards, >3 players = 5 cards)
        /// </summary>
        public CardController CurrentDeck
        {
            get
            {
                // returns the value of the private field currentDeck
                return currentDeck;
            }
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
        public IEnumerable<BasePlayer> Opponents { get; set; }

        /// <summary>
        /// Property to indicate that this player resigns or is busted somehow
        /// </summary>
        public bool Busted { get; set; }



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
        public BasePlayer(CardController currentDeck)
        {
            // This is dependency injection, the creator of the player also give a deck of cards that the player should use
            CurrentDeck = currentDeck;
        }

        /// <summary>
        /// Abstract method for Play
        /// Each player should decide which opponent to ask for a card
        /// If no card is given player calls CardController.PullOne
        /// If PullOne returns null there are no cards left in the deck.
        /// </summary>
        public abstract void Play();

        public IEnumerable<Card> GetCards(Values value)
        {
            // TODO! If this player is has the card then remove it from hand and return it.


            return Hand.Where(card => card.Value == value);
        }

        public void OtherPlayersPlayed(BasePlayer cardReciever, BasePlayer cardSender, Values cardValue, IEnumerable<Card> returnResult)
        {
            var reciever = cardReciever.GetType().Name;
            var sender = cardSender.GetType().Name;
            if (SwappedCards.ContainsKey(reciever) == false)
            {
                SwappedCards.Add(reciever, new List<Card>());
            }
            SwappedCards[reciever].AddRange(returnResult);

            if (SwappedCards.ContainsKey(sender))
            {
                foreach (var card in returnResult)
                {
                    // BUG! doesn't remove cards that have been taken.
                    var remove = SwappedCards[sender].Where(c => c.Suit == card.Suit && c.Value == card.Value).FirstOrDefault();
                    if (remove != null)
                    {
                        SwappedCards[sender].Remove(remove);
                    }
                }
            }
        }
    }
}
