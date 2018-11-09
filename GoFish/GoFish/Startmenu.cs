using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    public class Startmenu
    {
        string tsetM;
        public static Startmenu Create()
        {
            
            return new Startmenu();
        }

        public void Run()
        {
            
            string beginMessage = "Börja spelet";
            string exitMessage = "Avsluta spelet";
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
                    Game game = new Game();

                    game.Initialize();
                }
                else if (s.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        private static int GetNmbrOfPlayers()
        {
            int nmbrOfPlayers = 0;
            string message = "Choose a numerical value between 2 and 4";
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

    }
}