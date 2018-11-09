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
        public Card(Suit suit, Values value)
        {
            this.suit = suit;
            this.value = value;
        }

        public Suit suit;
        public Values value;

        public override string ToString()
        {
            return $"{suit} {(Values)value}";
            
        }
    }
}