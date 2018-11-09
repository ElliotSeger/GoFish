using System;

namespace GoFish
{

    public enum Suit
    {
        Hjärter,
        Ruter,
        Spader,
        Klöver
    }

    public enum Values
    {
        Noll,
        Ess,
        Två,
        Tre,
        Fyra,
        Fem,
        Sex,
        Sju,
        Åtta,
        Nia,
        Tio,
        Knekt,
        Dam,
        Kung
    }

    public class Card
    {
        public Card(Card card)
        {
            this.suit = card.suit;
            this.value = card.value;
        }

        public Suit suit;
        public int value;

        public override string ToString()
        {
            return $"{suit} {(Values)value}";
            
        }
    }
}