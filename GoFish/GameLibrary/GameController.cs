using System;
using System.Collections.Generic;
using System.Linq;
using CardLibrary;
using Interfaces;
using PlayerLibrary;

namespace GameLibrary
{
    public class GameController
    {
        private IEnumerable<IBasePlayer> players;
        private CardController cardController;
        private PlayerController playerController;
        private readonly IGenericViewModel viewModel;
        private readonly IStartMenu startMenu;


        /// <summary>
        /// Runs when the application is started.
        /// </summary>
        public GameController(IStartMenu startMenu, IGenericViewModel viewModel, CardController cardController, PlayerController playerController)
        {
            this.startMenu = startMenu;
            this.viewModel = viewModel;
            this.cardController = cardController;
            this.playerController = playerController;
        }

        /// <summary>
        /// Begins the game and creates the players.
        /// </summary>
        public void Run()
        {
            // Get players and initialize them with deck and opponents
            players = startMenu.Execute();

            foreach (IBasePlayer player in players)
            {
                // Assign the deck to each player so they could pull cards
                player.CurrentDeck = cardController;
                // Make sure each player gets aware of the opponents
                player.Opponents = players.Where(p => p.PlayerName != player.PlayerName);
                // Attach a callback delegate to each player to enable callback when cardswapping happens
                player.CardExchangeAnnouncementCallback += AnnotatePlayersOfCardExchange;
            }


            IBasePlayer winner = null;
            while (winner == null)
            {
                for (int current = 0; current < players.Count(); current++)
                {
                    IBasePlayer currentPlayer = players.ToArray()[current];

                    // If player has 0 cards on hand and deck has 0 cards left then skip to next player
                    // continue will continue with next value of the for loop
                    if (currentPlayer.CardsLeftOnHand == 0 && currentPlayer.CurrentDeck.CardsLeft == 0)
                        continue;

                    currentPlayer.Play();
                }

                if (AllPlayersOutOfCards(players))
                {
                    winner = players.OrderByDescending(p => p.OnTheTable.Count).First();
                    Console.WriteLine($"Winner is: {winner.PlayerName}");
                    Console.WriteLine($"OnTheHand: {winner.CardsLeftOnHand} cards");
                    Console.WriteLine($"OnTheTable: {winner.OnTheTable.Count} cards");

                    foreach (var player in players.OrderByDescending(p=>p.OnTheTable.Count))
                    {
                        Console.WriteLine($"Player: {player.PlayerName}, {player.OnTheTable.Count} on the table, {player.CardsLeftOnHand} on the hand.");
                    }
                }
            }
        }

        private bool AllPlayersOutOfCards(IEnumerable<IBasePlayer> players)
        {
            // Compare count of all players with more than 0 cards on hand with 0
            return players.Where(p => p.CardsLeftOnHand > 0).Count() == 0;
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
        public void AnnotatePlayersOfCardExchange(IBasePlayer cardReciever, IBasePlayer cardSender, Values cardValue, IEnumerable<Card> returnResult)
        {
            // Tell every player that two players have asked for and handed over cards.
            // This is just to let all players know that a card swap has happened and what
            // cards where swapped.
            // ReturnResult is the cards that were handed over. cardValue is the value which was asked for. 
            foreach (IBasePlayer player in players)
            {
                player.OtherPlayersPlayed(cardReciever, cardSender, cardValue, returnResult);
            }
        }
    }
}

