using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    class Game
    {
        Deck deck;

        public Game()
        {
            Initialize();
        }
        public void Initialize()
        {
            deck = new Deck();
        }
    }
}

