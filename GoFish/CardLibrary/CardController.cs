using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class CardController
    {
        List<Card> cards = new List<Card>();

        public int CardsLeft
        {
            get
            {
                return cards.Count;
            }
        }

        public CardController()
        {
            Initialize();
        }

        /// <summary>
        /// Clears the deck and then creates a new one which is then shuffled.
        /// </summary>
        public void Initialize()
        {
            cards.Clear();
            for (int suit = 0; suit < 4; suit++)
            {
                for (int value = 1; value < 14; value++)
                {
                    cards.Add(new Card((Suits)suit, (Values)value));
                }
            }
            Shuffle();
        }

        /// <summary>
        /// Shuffles the cards at the start of the game.
        /// </summary>
        public void Shuffle()
        {
            Random cardShuffle1 = new Random();

            for (int x = 0; x < cards.Count(); x++)
            {
                for (int y = 0; y < cards.Count(); y++)
                {
                    int card1 = cardShuffle1.Next(0, 51);
                    int card2 = cardShuffle1.Next(0, 51);
                    Card temp = cards[card1];
                    cards[card1] = cards[card2];
                    cards[card2] = temp;
                    // Console.WriteLine($"Byter kort {cards[card1]} med {cards[card2]}");
                }
            }
        }

        /// <summary>
        /// Pulls one card when called upon.
        /// </summary>
        /// <returns>
        /// Returns a card unless the pile is empty.
        /// </returns>
        public Card PullOne()
        {
            if (cards.Count == 0)
            {
                return null;
            }
            Card card = cards[0];
            // Ser till att det alltid finns ett kort kvar i leken.
            cards.RemoveAt(0);
            return card;
        }

        /// <summary>
        /// Separation Of Concern(SOC), by using SOC we have PullMany inside Deck instead of inside the Game.
        /// </summary>
        /// <param name="nmbrOfCards">
        /// The number of cards to be drawn.
        /// </param>
        /// <returns></returns>
        public IEnumerable<Card> PullMany(int nmbrOfCards)
        {
            List<Card> result = new List<Card>();
            if (cards.Count() >= nmbrOfCards)
            {
                for (int i = 0; i < nmbrOfCards; i++)
                {
                    result.Add(PullOne());
                }
            }
            // Sorting the hand based on cards Value
            result.Sort((x, y) => x.Value.CompareTo(y.Value));
            return result;
        }
    }


}
