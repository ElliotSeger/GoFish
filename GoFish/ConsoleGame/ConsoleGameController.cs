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
        private CardController deck;
       private ConsoleStartmenu startmenu;
        private PlayerController playerController;


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
            deck = new CardController();
            playerController = new PlayerController();
            startmenu = new ConsoleStartmenu(playerController);
        }

        /// <summary>
        /// Begins the game and creates the players.
        /// </summary>
        public void Run()
        {
            players = startmenu.Execute();
            foreach (var player in players)
            {
                player.CurrentDeck = deck;
                player.Opponents = players.Where(p => p.GetType().Name != player.GetType().Name);
            }
        }
    }
}

