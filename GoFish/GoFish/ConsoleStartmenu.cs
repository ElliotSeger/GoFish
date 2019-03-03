using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using PlayerLibrary;

namespace GoFish
{
    public class ConsoleStartmenu : IStartMenu
    {
        public IEnumerable<string> Players { get; private set; }
        private readonly PlayerController playerController;
        private readonly IGenericViewModel viewModel;

        public ConsoleStartmenu(PlayerController playerController, IGenericViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.playerController = playerController;
            Players = playerController.GetPlayers();
        }

        /// <summary>
        /// Begins the game and creates the players with the given input by the user.
        /// </summary>
        /// <returns>
        /// Returns an IEnumerable with the players unless the user presses escape in which case it returns null.
        /// </returns>
        public IEnumerable<IBasePlayer> Execute() =>
            // TODO! This return will be removed later.
            // TODO! This method must create startmenu and allow plyer selection
            new List<BasePlayer>
            {
                playerController.InstantiatePlayer("Player1"),
                playerController.InstantiatePlayer("Player2")//,
                //playerController.InstantiatePlayer("Player3")
            };//Console.CursorVisible = false;//Menu.Option[] options =//{//    new Menu.Option("Start", true, Menu.Start),//    new Menu.Option("Exit", false, Menu.Exit)//};//Menu.GetAction(options)();//int nmbrOfPlayers;//while (true)//{//    ConsoleKeyInfo s = Console.ReadKey(true);//    if (s.Key == ConsoleKey.Enter)//    {//        nmbrOfPlayers = GetNmbrOfPlayers();//        return CreatePlayers(nmbrOfPlayers);//    }//    else if (s.Key == ConsoleKey.Escape)//    {//        return null;//    }//}

        private int GetNmbrOfPlayers()
        {
            foreach (string p in Players)
            {
                Console.WriteLine(p);
            }
            int nmbrOfPlayers = 0;
            string message = $"Choose a numerical value between 2 and {Players.Count()}";
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, 7);
            Console.WriteLine(message);
            Console.SetCursorPosition((Console.WindowWidth) / 2, 8);
            while (!int.TryParse(Console.ReadLine(), out nmbrOfPlayers) && (nmbrOfPlayers < 2 || nmbrOfPlayers > 4))
            {
                Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, 7);
                Console.WriteLine(message);
                Console.SetCursorPosition((Console.WindowWidth) / 2, 8);
            }
            return nmbrOfPlayers;
        }

        private IEnumerable<BasePlayer> CreatePlayers(int nmbrOfPlayers)
        {
            List<BasePlayer> result = new List<BasePlayer>();
            for (int i = 0; i < nmbrOfPlayers; i++)
            {
                int playerNo = 0;
                string message = $"Choose player {i + 1}?";

                while (!int.TryParse(Console.ReadLine(), out playerNo) || playerNo < 1 || playerNo > Players.Count())
                {
                    Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, 7);
                    Console.WriteLine(message);
                    Console.SetCursorPosition((Console.WindowWidth) / 2, 8);
                }

                result.Add(playerController.InstantiatePlayer(Players.ElementAt(playerNo)));
            }
            return result;
        }
    }
}