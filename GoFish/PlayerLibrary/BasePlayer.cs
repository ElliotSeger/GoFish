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
        public Deck CurrentDeck { get; set; }
        public List<Card> Hand { get; set; }

        public BasePlayer()
        {

        }

        /// <summary>
        /// Sets the players deck when called upon.
        /// </summary>
        /// <param name="currentDeck">
        /// The deck that the CurrentDeck is set as.
        /// </param>
        public BasePlayer(Deck currentDeck)
        {
            CurrentDeck = currentDeck;
        }


        public void Play()
        {

        }
    }
}
