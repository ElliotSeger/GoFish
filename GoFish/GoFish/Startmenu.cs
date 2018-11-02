using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFish
{
    static class Startmenu
    {
        public static void Start()
        {
            Console.CursorVisible = true;

            string begin = "Enter för att börja spelet";
            string exit = "Escape för att avsluta spelet";
            Console.SetCursorPosition((Console.WindowWidth - begin.Length) / 2, 10);
            Console.WriteLine(begin);
            Console.SetCursorPosition((Console.WindowWidth - exit.Length) / 2, 11);
            Console.WriteLine(exit);

            ConsoleKeyInfo s = Console.ReadKey();

            if (s.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                string players = "Skriv in antal spelare från 2 till 4";
                Console.SetCursorPosition((Console.WindowWidth - players.Length) / 2, 5);
                Console.WriteLine(players);
                
                Console.SetCursorPosition((Console.WindowWidth - players.Length) / 2, 6);
            }

            if (s.Key == ConsoleKey.Escape)
            {
                System.Environment.Exit(0);
            }

            Console.ReadKey();
        }
    }
}
