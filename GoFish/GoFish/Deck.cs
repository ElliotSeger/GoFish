using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    public class Deck
    {
        List<Card> cards = new List<Card>();

        public Deck()
        {
            Initialize();
        }

        public void Initialize()
        {
            cards.Clear();
            for (int suit = 0; suit < 4; suit++)
            {
                for (int value = 1; value < 14; value++)
                {
                    cards.Add(new Card ((Suit)suit, (Values)value ));
                }
            }
            Shuffle();
        }

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
                    //Console.WriteLine($"byter kort {cards[card1]} med {cards[card2]}");
                }
            }
        }

        public Card Pull()
        {
            if (cards.Count == 0)
            {

                return null;
            }
            Card card = cards[0];
            // se till att det alltid finns ett kort kvar i leken
            cards.RemoveAt(0);
            return card;
        }
    }


}
