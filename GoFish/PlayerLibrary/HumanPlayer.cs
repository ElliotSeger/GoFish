using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                foreach (var opponent in Opponents)
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
            return Values.Dam;
        }
    }
}
