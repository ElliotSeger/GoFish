using ConsoleGame;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GoFish
{
    class Program
    {
        static void Main(string[] args)
        {
            IGenericViewModel vm = new ConsoleViewModel();
            ConsoleGameController game = new ConsoleGameController(vm);
            game.Run();
        }
    }
}
