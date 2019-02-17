using System;
using System.Linq;
using CardLibrary;

namespace PlayerLibrary
{
    public class HumanPlayer : BasePlayer
    {

        /// <summary>
        /// Empty constructor needs to exist to be able to create an object without injecting a deck
        /// </summary>
        public HumanPlayer() : base()
        { }

        /// <summary>
        /// Sets the players deck when called upon. See further in <see cref="CurrentDeck"/>
        /// </summary>
        /// <param name="currentDeck">
        /// The deck that the CurrentDeck is set as.
        /// </param>
        public HumanPlayer(CardController currentDeck) : base(currentDeck)
        { }

        public override BasePlayer SelectOpponent()
        {
            int selected = -1;
            do
            {
                int selection = 1;
                foreach (BasePlayer opponent in Opponents)
                {
                    Console.WriteLine($"Player number {selection}: {opponent.PlayerName}");
                    selection++;
                }
                Console.WriteLine("select which player to ask.");
            } while (!int.TryParse(Console.ReadLine(), out selected) || selected < 1 || selected > Opponents.Count());
            return Opponents.ElementAt(selected - 1);
        }

        public override Values SelectValueToAskFor()
        {
            //Detta grupperar dina Values i fallande ordning efter hur många du har av just det Values på handen
            //valueGroups kommer att innehålla en IEnumerable av KeyValue där Key är kortets Value och Value är hur många du har av det
            var valueGroups = Hand
                //Den bortersta new {Key = key, Value = cards.Count} ändrar datatypen till att vara dictionaryelement 
                //där key är valören och value år lika med hur många du har av just det kortet
                .GroupBy(key => key.Value, source => source, (key, cards) => new { Key = key, Value = cards.Count() })
                //Sortera alla element efter hur många du har av var och en. Om du i stället använder v => v.Key så får du dem i kortordning
                .OrderByDescending(v => v.Value)
                //Och sen returnera allt som en IEnumerable i stället för en dictionary
                .Select(v => new { v.Key, v.Value });

            //Loopa och visa vilka valörer du har och hur många av respektive
            foreach (var valueGroup in valueGroups)
            {
                Console.WriteLine($"You got {valueGroup.Value} cards of Value {valueGroup.Key}");
            }

            //Fråga efter ett Values, anges som en sträng (Ess, Kung, Trea osv)
            Console.WriteLine($"Select a value to ask for:");
            string selectedValue = Console.ReadLine();

            //Konvertera den inmatade strängen till ett riktig Values, du kan använda TryParse här också...
            Values result = (Values)Enum.Parse(typeof(Values), selectedValue);

            //returnera det valda values (valör)
            return result;
        }
    }
}
