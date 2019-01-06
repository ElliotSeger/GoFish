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
        public override void Play()
        {
            // TODO! This code is experimental, needs to be rewritten
            // TODO! better way to pick an opponent
            var opponent = Opponents.First();

            // holder for cards returned from opponent
            var cards = new List<Card>();

            // TODO! Player must own at least one card of the value they ask for.
            // TODO! add a way to choose what card to ask for, right now hardcoded to Values.Ess
            var recieved = opponent.GetCards(Values.Ess);

            // Announce the swap of cards, Values.Ess shouldn't be hardcoded
            CardExchangeAnnouncement?.Invoke(this, opponent, Values.Ess, recieved);

            // Add any cards received to local storage
            cards.AddRange(recieved);

            // Test to see if any cards was returned
            if (cards.Count() == 0)
            {
                // Otherwise pull a card from the deck
                // TODO! How to handle end of cards in the deck?
                cards.Add(CurrentDeck.PullOne());
            }

            // Add whatever cards we have from local storage
            Hand.AddRange(cards);
        }
    }
}
