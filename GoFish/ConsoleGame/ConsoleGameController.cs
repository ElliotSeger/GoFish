using System;
using System.Collections.Generic;
using System.Linq;
using CardLibrary;
using Interfaces;
using PlayerLibrary;

namespace ConsoleGame
{
    public class ConsoleGameController
    {
        private IEnumerable<BasePlayer> players;
        private CardController deck;
        private ConsoleStartmenu startmenu;
        private PlayerController playerController;
        private IGenericViewModel vm;


        /// <summary>
        /// Runs when the application is started.
        /// </summary>
        public ConsoleGameController(IGenericViewModel vm)
        {
            this.vm = vm;
            Initialize();
        }

        /// <summary>
        /// Creates a startmenu and a new deck when called upon.
        /// </summary>
        public void Initialize()
        {
            deck = new CardController();
            playerController = new PlayerController(vm);
            startmenu = new ConsoleStartmenu(playerController);
        }


        /// <summary>
        /// Begins the game and creates the players.
        /// </summary>
        public void Run()
        {
            // Get players and initialize them with deck and opponents
            players = startmenu.Execute();

            foreach (BasePlayer player in players)
            {
                // Assign the deck to each player so they could pull cards
                player.CurrentDeck = deck;
                // Make sure each player gets aware of the opponents
                player.Opponents = players.Where(p => p.GetType().Name != player.GetType().Name);
                // Attach a callback delegate to each player to enable callback when cardswapping happens
                player.CardExchangeAnnouncement += AnnotatePlayersOfCardExchange;
            }


            BasePlayer winner = null;
            while (winner == null)
            {
                for (int current = 0; current < players.Count(); current++)
                {
                    //TODO! Test for empty deck
                    //TODO! If empty deck, test for different player amount of cards in OnTheTable
                    BasePlayer currentPlayer = players.ToArray()[current];
                    if (!currentPlayer.Play())
                    {
                        Console.WriteLine($"player {currentPlayer} is out");
                        players = players.Where(p => p.GetType().Name != currentPlayer.GetType().Name);
                        current--;
                    }
                    if (players.Count() == 1)
                    {
                        winner = players.ToArray()[0];
                        break;
                    }
                }
            }
            Console.WriteLine($"Winner is {winner}");
        }

        /// <summary>
        /// This will be called from all players that has the following statement executed:
        /// player.CardExchangeAnnouncement += AnnotatePlayersOfCardExchange;
        /// In each players Play() method there should be a callback to here, just to announce 
        /// the card swap between two players. It should look like:
        /// CardExchangeAnnouncement?.Invoke(this, opponent, value, recieved);
        /// Where "this" identifies the current player, "opponent" the player that is asked for the cards
        /// "value" is the card value requested and "recieved" is the cards recieved from the opponent
        /// </summary>
        /// <param name="cardReciever">The player asking for a card</param>
        /// <param name="cardSender">The player being asked</param>
        /// <param name="cardValue">Card value asked for</param>
        /// <param name="returnResult">Collection of cards that is returned from cardSender</param>
        public void AnnotatePlayersOfCardExchange(BasePlayer cardReciever, BasePlayer cardSender, Values cardValue, IEnumerable<Card> returnResult)
        {
            // Tell every player that two players have asked for and handed over cards.
            // ReturnResult is the cards that were handed over. cardValue is the value which was asked for. 
            foreach (BasePlayer player in players)
            {
                player.OtherPlayersPlayed(cardReciever, cardSender, cardValue, returnResult);
            }
        }
    }
}

