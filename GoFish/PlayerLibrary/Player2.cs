using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerLibrary
{
    public class Player2 : BasePlayer
    {
        /// <summary>
        /// Empty constructor needs to exist to be able to create an object without injecting a deck
        /// </summary>
        public Player2() : base()
        { }

        /// <summary>
        /// Sets the players deck when called upon. See further in <see cref="CurrentDeck"/>
        /// </summary>
        /// <param name="currentDeck">
        /// The deck that the CurrentDeck is set as.
        /// </param>
        public Player2(CardController currentDeck) : base(currentDeck)
        { }

        /// <summary>
        /// Concrete method for Play
        /// Each player should decide which opponent to ask for a card
        /// If no card is given player calls CardController.PullOne
        /// If PullOne returns null there are no cards left in the deck.
        /// </summary>
        public override void Play()
        {
            // TODO! Finish behaviour.
            // TODO! better way to pick an opponent
            var opponent = Opponents.First();
            var cards = new List<Card>();
            // TODO! add a way to choose the value to pick.
            // TODO! add a way to  keep check of/alert all players in the chat of which players values are being handed over to who
            // Player must own at least one card of the value they ask for.
            var recieved = opponent.GetCards(knownCard.Value);
            CardExchangeAnnouncement?.Invoke(this, opponent, knownCard.Value, recieved);
            cards.AddRange(recieved);
            if (cards.Count() == 0)
            {
                cards.Add(CurrentDeck.PullOne());
            }
            Hand.AddRange(cards);
        }
    }
}
