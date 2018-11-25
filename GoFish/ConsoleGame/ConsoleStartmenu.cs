using PlayerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class ConsoleStartmenu
    {
        public IEnumerable<Type> PlayerTypes { get; set; }

        /// <summary>
        /// Creates a ConsoleStartmenu when called upon.
        /// </summary>
        /// <returns></returns>
        public static ConsoleStartmenu Create()
        {
            return new ConsoleStartmenu();
        }

        private ConsoleStartmenu()
        {
            PlayerTypes = PlayerController.GetPlayers();

        }

        /// <summary>
        /// Begins the game and creates the players with the given input by the user.
        /// </summary>
        /// <returns>
        /// Returns an IEnumerable with the players unless the user presses escape in which case it returns null.
        /// </returns>
        public IEnumerable<BasePlayer> Execute()
        {
            string beginMessage = "Start Game";
            string exitMessage = "Exit Game";
            int nmbrOfPlayers;
            while (true)
            {
                Console.SetCursorPosition((Console.WindowWidth - beginMessage.Length) / 2, 5);
                Console.WriteLine(beginMessage);
                Console.SetCursorPosition((Console.WindowWidth - exitMessage.Length) / 2, 6);
                Console.WriteLine(exitMessage);

                ConsoleKeyInfo s = Console.ReadKey(true);

                if (s.Key == ConsoleKey.Enter)
                {
                    nmbrOfPlayers = GetNmbrOfPlayers();
                    return CreatePlayers(nmbrOfPlayers);
                }
                else if (s.Key == ConsoleKey.Escape)
                {
                    return null;
                }
            }
        }

        private int GetNmbrOfPlayers()
        {
            foreach (var p in PlayerTypes)
            {
                Console.WriteLine(p.Name);
            }
            int nmbrOfPlayers = 0;
            string message = $"Choose a numerical value between 2 and {PlayerTypes.Count()}";
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

                while (!int.TryParse(Console.ReadLine(), out playerNo) || playerNo < 1 || playerNo > PlayerTypes.Count())
                {
                    Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, 7);
                    Console.WriteLine(message);
                    Console.SetCursorPosition((Console.WindowWidth) / 2, 8);
                }

                result.Add((BasePlayer)Activator.CreateInstance(PlayerTypes.ToArray()[playerNo - 1]));
            }
            return result;
        }
    }
}