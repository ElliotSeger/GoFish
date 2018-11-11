using CardLibrary;
using ConsoleGame;
using PlayerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class ConsoleGameController
    {
        private IEnumerable<Player> players;
        private Deck deck;
       private ConsoleStartmenu startmenu;

        public ConsoleGameController()
        {
            Initialize();
        }

        public void Initialize()
        {
            startmenu = ConsoleStartmenu.Create();
            deck = Deck.Create();
        }

        public void Run()
        {
            players = startmenu.Execute();
            Player player1 = new Player(deck);
            player1.Play();
        }
    }
}

