using System;
using System.Linq;

namespace ConsoleGame
{
    internal static class Menu
    {
        public struct Option
        {
            public Option(string text, bool selected, Action function)
            {
                this.text = text;
                this.selected = selected;
                this.function = function;
            }

            public string text;
            public bool selected;
            public Action function;
        }

        public static void Start()
        {
            while (true)
            {

            }
        }

        public static void Exit() => Environment.Exit(0);

        private static void MainMenu(string[] args) => Console.CursorVisible = false;

        public static Action GetAction(Option[] options)
        {
            do
            {
                int startY = 3;
                for (int i = 0; i < options.Length; ++i)
                {
                    DrawOption(options[i], startY + i * 2);
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.S)
                {
                    MoveSelection(ref options, true);
                }
                else if (keyInfo.Key == ConsoleKey.W)
                {
                    MoveSelection(ref options, false);
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return options.Where(option => option.selected).First().function;
                }
            } while (true);
        }

        private static void MoveSelection(ref Option[] options, bool forward)
        {
            for (int i = 0; i < options.Length; ++i)
            {
                if (options[i].selected)
                {
                    options[i].selected = false;
                    int nextIndex = i + (forward ? 1 : -1);
                    if (i == (forward ? (options.Length - 1) : 0))
                    {
                        nextIndex = (forward ? 0 : (options.Length - 1));
                    }
                    options[nextIndex].selected = true;
                    break;
                }
            }
        }

        private static void DrawOption(Option option, int yPosition)
        {
            int centerX = Console.BufferWidth / 2;
            Console.SetCursorPosition(centerX - option.text.Length / 2, yPosition);

            if (option.selected)
            {
                ConsoleColor initialForeground = Console.ForegroundColor;
                ConsoleColor initialBackground = Console.BackgroundColor;

                Console.ForegroundColor = initialBackground;
                Console.BackgroundColor = initialForeground;

                Console.Write($" {option.text} ");

                Console.ForegroundColor = initialForeground;
                Console.BackgroundColor = initialBackground;
            }
            else
            {
                Console.Write($" {option.text} ");
            }
        }
    }
}
