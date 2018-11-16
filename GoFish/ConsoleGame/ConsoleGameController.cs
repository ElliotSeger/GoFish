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
        private IEnumerable<BasePlayer> players;
        private Deck deck;
       private ConsoleStartmenu startmenu;

        /// <summary>
        /// Runs when the application is started.
        /// </summary>
        public ConsoleGameController()
        {
            Initialize();
        }

        /// <summary>
        /// Creates a startmenu and a new deck when called upon.
        /// </summary>
        public void Initialize()
        {
            startmenu = ConsoleStartmenu.Create();
            deck = Deck.Create();
        }

        /// <summary>
        /// Begins the game and creates the players.
        /// </summary>
        public void Run()
        {
            players = startmenu.Execute();
            BasePlayer player1 = new BasePlayer(deck);
            player1.Play();
        }
    }
}

