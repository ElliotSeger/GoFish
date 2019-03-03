using System;
using System.Collections.Generic;
using System.Linq;
using CardLibrary;
using Interfaces;

namespace GoFish
{
    public class ConsoleViewModel : IGenericViewModel
    {
        public IBasePlayer SelectOpponent(IEnumerable<IBasePlayer> opponents)
        {
            int selected = -1;
            do
            {
                int selection = 1;
                foreach (IBasePlayer opponent in opponents)
                {
                    Console.WriteLine($"Player number {selection}: {opponent.PlayerName}");
                    selection++;
                }
                Console.WriteLine("select which player to ask.");
            } while (!int.TryParse(Console.ReadLine(), out selected) || selected < 1 || selected > opponents.Count());
            return opponents.ElementAt(selected - 1);
        }

        public Values SelectCardValue(IEnumerable<Card> hand)
        {
            var valueGroups = hand
                .GroupBy(key => key.Value, source => source, (key, cards) => new { Key = key, Value = cards.Count() })
                .OrderByDescending(v => v.Value)
                .Select(v => new { v.Key, v.Value });

            foreach (var valueGroup in valueGroups)
            {
                Console.WriteLine($"You got {valueGroup.Value} cards of Value {valueGroup.Key}");
            }

            string selectedValue = string.Empty;
            Values result = Values.Noll;
            do
            {
                do
                {
                    Console.WriteLine($"Select a value to ask for:");
                    selectedValue = Console.ReadLine();
                } while (!Enum.TryParse(selectedValue, true, out result) || !Enum.IsDefined(typeof(Values), result));

            } while (/*result < Values.Noll || result > Values.Kung || */!valueGroups.Any(v => v.Key == result));

            return result;
        }

        public void ShowMessage(string message) => Console.WriteLine(message);

    }
}
