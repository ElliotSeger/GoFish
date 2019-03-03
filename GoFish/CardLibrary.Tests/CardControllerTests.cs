using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardLibrary.Tests
{
    [TestClass()]
    public class CardControllerTests
    {
        [TestMethod()]
        public void FromStartADeckContains52Cards()
        {
            //arrange
            CardController cardcontroller = new CardController();
            int expectedCardcount = 52;

            //act
            int actualCardcount = cardcontroller.CardsLeft;

            //assert
            Assert.AreEqual(expectedCardcount, actualCardcount);
        }

        [TestMethod()]
        public void Pull10FromFullDeckWillReturn42()
        {
            //arrange
            CardController cardcontroller = new CardController();
            int expectedCardsBefore = 52;
            int expectedCardsPulled = 10;
            int expectedCardsAfter = expectedCardsBefore - expectedCardsPulled;

            //act
            int actualCardsBefore = cardcontroller.CardsLeft;
            IEnumerable<Card> cards = cardcontroller.PullMany(expectedCardsPulled);
            int actualCardsPulled = cards.Count();
            int actualCardsAfter = cardcontroller.CardsLeft;

            //assert
            Assert.AreEqual(expectedCardsBefore, actualCardsBefore);
            Assert.AreEqual(expectedCardsAfter, actualCardsAfter);
            Assert.AreEqual(expectedCardsPulled, actualCardsPulled);
        }

        [TestMethod()]
        public void Pull1FromFullDeckWillReturn51()
        {
            //arrange
            CardController cardcontroller = new CardController();
            int expectedCardsBefore = 52;
            int expectedCardsAfter = expectedCardsBefore - 1;

            //act
            int actualCardsBefore = cardcontroller.CardsLeft;
            Card card = cardcontroller.PullOne();
            int actualCardsAfter = cardcontroller.CardsLeft;

            //assert
            Assert.AreEqual(expectedCardsBefore, actualCardsBefore);
            Assert.AreEqual(expectedCardsAfter, actualCardsAfter);
            Assert.IsInstanceOfType(card, typeof(Card));
        }
    }
}