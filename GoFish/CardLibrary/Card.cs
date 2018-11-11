using System;

namespace CardLibrary
{
    public class Card
    {
        public Card(Suits suit, Values value)
        {
            this.Suit = suit;
            this.Value = value;
        }

        public Suits Suit { get; set; }
        public Values Value { get; set; }

        public override string ToString()
        {
            return $"{Suit} {(Values)Value}";
            
        }
    }
}