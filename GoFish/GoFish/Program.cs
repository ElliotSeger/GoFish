using System;
using CardLibrary;
using ConsoleGame;
using GameLibrary;
using Interfaces;
using PlayerLibrary;

namespace GoFish
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // The objects that are represented by interfaces are object that are platform dependent

            IGenericViewModel viewModel = new ConsoleViewModel();
            CardController cardController = new CardController();
            PlayerController playerController = new PlayerController(viewModel);
            IStartMenu startMenu = new ConsoleStartmenu(playerController, viewModel);
            GameController game = new GameController(startMenu, viewModel, cardController, playerController);
            game.Run();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
