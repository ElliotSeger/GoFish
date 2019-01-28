using System;
using System.Collections.Generic;
using System.Linq;
using CardLibrary;

namespace PlayerLibrary
{
    public class Player3 : BasePlayer
    {
        /// <summary>
        /// Empty constructor needs to exist to be able to create an object without injecting a deck
        /// </summary>
        public Player3() : base()
        { }

        /// <summary>
        /// Sets the players deck when called upon. See further in <see cref="CurrentDeck"/>
        /// </summary>
        /// <param name="currentDeck">
        /// The deck that the CurrentDeck is set as.
        /// </param>
        public Player3(CardController currentDeck) : base(currentDeck)
        { }

        /// <summary>
        /// Concrete method for Play
        /// Each player should decide which opponent to ask for a card
        /// If no card is given player calls CardController.PullOne
        /// If PullOne returns null there are no cards left in the deck.
        /// If method returns false then player failed to get a card from opponent or deck
        /// </summary>
        public override bool Play()
        {
            //Strategic selection of opponent
            BasePlayer opponent = SelectOpponent();
            //Strategic selection of card value
            Values valueToAskFor = SelectValueToAskFor();

            Console.WriteLine($"{PlayerName} asks {opponent.PlayerName} for a {valueToAskFor}");
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

                Console.WriteLine($"{PlayerName} didn't recieve any {valueToAskFor} from player {opponent.PlayerName}, pulled one from deck instead, {CurrentDeck.CardsLeft} left in deck\n");
            }
            else
            {
                Console.WriteLine($"{PlayerName} got {recieved.Count()} {valueToAskFor} from {opponent.PlayerName}\n");
            }

            return true;
        }

        //TODO! Make a strategic selection of opponent to ask for cards
        private BasePlayer SelectOpponent() =>
            // TODO! This code is experimental, needs to be rewritten
            // TODO! better way to pick an opponent
            Opponents.First();

        //TODO! Make a strategic selection of card value to ask for
        private Values SelectValueToAskFor()
        {
            // TODO! add a way to choose what card to ask for, 
            // right now hardcoded to the value that is represented most
            Dictionary<Values, IEnumerable<Card>> valueGroups = Hand
                .GroupBy(key => key.Value, source => source, (key, cards) => new { Key = key, Value = cards })
                .OrderByDescending(s => s.Value.Count())
                .ToDictionary(g => g.Key, g => g.Value);
            // If valueGroups.Count() == 0 så är handen tom

            return valueGroups.First().Key;
        }
    }
}
