using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLibrary
{
    public class BasePlayer
    {
        public CardController CurrentDeck
        {
            get { return currentDeck; }
            set
            {
                currentDeck = value;
                Hand = new List<Card>();
                Hand.AddRange(currentDeck.PullMany(7));
            }
        }
        private CardController currentDeck;
        public List<Card> Hand { get; set; }
        public IEnumerable<BasePlayer> Opponents { get; set; }

        public BasePlayer()
        {

        }

        /// <summary>
        /// Sets the players deck when called upon.
        /// </summary>
        /// <param name="currentDeck">
        /// The deck that the CurrentDeck is set as.
        /// </param>
        public BasePlayer(CardController currentDeck)
        {
            CurrentDeck = currentDeck;
        }


        public void Play()
        {

        }
    }
}
