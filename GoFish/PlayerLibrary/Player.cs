using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLibrary
{
    public class Player
    {
        public Deck CurrentDeck { get; }
        public List<Card> Hand { get; set; }

        public Player(Deck currentDeck)
        {
            CurrentDeck = currentDeck;
        }

        public void Play()
        {

        }

    }
}
