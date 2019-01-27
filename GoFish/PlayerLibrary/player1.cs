using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerLibrary
{
    public class Player1 : BasePlayer
    {
        /// <summary>
        /// Empty constructor needs to exist to be able to create an object without injecting a deck
        /// </summary>
        public Player1() : base()
        { }

        /// <summary>
        /// Sets the players deck when called upon. See further in <see cref="CurrentDeck"/>
        /// </summary>
        /// <param name="currentDeck">
        /// The deck that the CurrentDeck is set as.
        /// </param>
        public Player1(CardController currentDeck) : base(currentDeck)
        { }

        /// <summary>
        /// Concrete method for Play
        /// Each player should decide which opponent to ask for a card
        /// If no card is given player calls CardController.PullOne
        /// If PullOne returns null there are no cards left in the deck.
        /// </summary>
        public override bool Play()
        {
            // TODO! This code is experimental, needs to be rewritten
            // TODO! better way to pick an opponent
            var opponent = Opponents.First();
            // lägg till val av opponent.

            // holder for cards returned from opponent
            var cards = new List<Card>();

            // TODO! Player must own at least one card of the value they ask for.
            // TODO! add a way to choose what card to ask for, 
            // right now hardcoded the the value that is represented most
            var valueGroups = this.Hand
                .GroupBy(c => c.Value, p => p, (key, g) => new { Value = key, Values = g.ToList() })
                .OrderByDescending(s => s.Values.Count)
                .ToDictionary(s => s.Value);
            // If valueGroups.Count() == 0 så är handen tom
            var askForCard = valueGroups.First().Key;
            Console.WriteLine($"{this.PlayerName} asks {opponent.PlayerName} for a {askForCard}");
            var recieved = opponent.GetCards(askForCard);
            // lägg till val av kort.

            // Announce the swap of cards, random card 'askForCard' shouldn't be hardcoded
            CardExchangeAnnouncement?.Invoke(this, opponent, askForCard, recieved);

            // Test to see if any cards was returned
            if (recieved.Count() == 0)
            {
                // Otherwise pull a card from the deck
                if (CurrentDeck.CardsLeft == 0)
                {
                    // No cards left in the deck, you're out!
                    return false;
                }
                Hand.Add(CurrentDeck.PullOne());
                // Sorting the hand based on cards Value
                Hand.Sort((x, y) => x.Value.CompareTo(y.Value));

                Console.WriteLine($"No cards received from player {opponent.PlayerName}, pulled one from deck, {CurrentDeck.CardsLeft} left\n");
            }
            else
            {
                Console.WriteLine($"{this.PlayerName} got {recieved.Count()} cards from {opponent.PlayerName}\n");
            }

            //TODO Look if we have four of some value and then drop them

            return true;
        }
    }
}
