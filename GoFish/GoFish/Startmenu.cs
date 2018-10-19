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
            string begin = "Börja spelet";
            string exit = "Avsluta spelet";
            Console.SetCursorPosition((Console.WindowWidth - begin.Length) / 2, 5);
            Console.WriteLine(begin);
            Console.SetCursorPosition((Console.WindowWidth - exit.Length) / 2, 6);
            Console.WriteLine(exit);
            
            //Console.WriteLine("Enter för att spela.");
            //Console.WriteLine("Escape för att avsluta.");
            ConsoleKeyInfo s = Console.ReadKey();

            if (s.Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Skriv in antal spelare från 2 till 4.");
                string nbrOfPlayers = Console.ReadLine();
            }

            if (s.Key == ConsoleKey.Escape)
            {
                System.Environment.Exit(0);
            }

            Console.WriteLine("I will code good things!");

            Console.ReadKey();
        }
    }
}
