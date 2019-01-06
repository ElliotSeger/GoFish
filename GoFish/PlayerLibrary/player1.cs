using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace PlayerLibrary
{
    public class Player1 : BasePlayer
    {
        public Player1()
        {
        }

        public Player1(CardController currentDeck) : base(currentDeck)
        {
        }
    }
}
