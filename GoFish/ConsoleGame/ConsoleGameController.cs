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
            // Get players and initialize them with deck and opponents
            players = startmenu.Execute();
            foreach (var player in players)
            {
                player.CurrentDeck = deck;
                player.Opponents = players.Where(p => p.GetType().Name != player.GetType().Name);
                player.CardExchangeAnnouncement += AnnotatePlayersOfCardExchange;
                // HACK! to test card swapping
                player.knownCard = (from p in players where p.GetType().Name == "Player1" select p.Hand.First()).First();

            }
            

            BasePlayer winner = null;
            while (winner == null)
            {
                for (int current = 0; current < players.Count(); current++)
                {
                    var currentPlayer = players.ToArray()[current];
                    currentPlayer.Play();
                }


            }
            Console.WriteLine($"Winner is {winner}");
        }

        public void AnnotatePlayersOfCardExchange(BasePlayer cardReciever, BasePlayer cardSender, Values cardValue, IEnumerable<Card> returnResult)
        {
            // Tell every player except cardReciever & cardSender that these two players have asked for and handed over cards.
            // ReturnResult is the cards that were handed over. cardValue is the value which was asked for. 
            //foreach (var player in players.Where(p => p.GetType().Name != cardReciever.GetType().Name && p.GetType().Name != cardSender.GetType().Name))
            foreach (var player in players)
                {
                player.OtherPlayersPlayed(cardReciever, cardSender, cardValue, returnResult);
            }
        }
    }
}

