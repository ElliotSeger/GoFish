using System;

namespace CardLibrary
{
    public class Card
    {
        /// <summary>
        /// Sets the cards values.
        /// </summary>
        /// <param name="suit"><
        /// Sets the suit of the card.
        /// /param>
        /// <param name="value">
        /// Sets the value of the card.
        /// </param>
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