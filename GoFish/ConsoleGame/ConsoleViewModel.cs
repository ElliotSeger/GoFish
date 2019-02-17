using PlayerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public delegate void ViewOpponentDelegate(IEnumerable<BasePlayer> opponents);

    public class ConsoleViewModel : IGenericViewModel
    {
        public void ViewOpponent(IEnumerable<BasePlayer> opponents)
        {
            foreach (var opponent in opponents)
            {
                Console.WriteLine($"Opponent: {opponent.PlayerName}");
            }
        }
    }
}
