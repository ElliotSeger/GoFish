using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Tests
{
    [TestClass()]
    public class CardControllerTests
    {
        [TestMethod()]
        public void Pull10FromFullDeckWillReturn42()
        {
            //arrange
            CardController cardcontroller = new CardController();
            int expectedCardcountBefore = 52;
            int cardsToPull = 10;
            int expectedCardcountAfter = expectedCardcountBefore - cardsToPull;

            //act
            int actualCardcountBefore = cardcontroller.CardsLeft;
            var cards = cardcontroller.PullMany(cardsToPull);
            int actualCardcountAfter = cardcontroller.CardsLeft;

            //assert
            Assert.AreEqual(expectedCardcountBefore, actualCardcountBefore);
            Assert.AreEqual(expectedCardcountAfter, actualCardcountAfter);
            Assert.AreEqual(cardsToPull, cards.Count());
        }
    }
}