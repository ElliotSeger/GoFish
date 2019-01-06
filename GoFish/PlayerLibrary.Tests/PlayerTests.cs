using System;
using System.Collections.Generic;
using CardLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayerLibrary.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestPlayer1PullsOneAceFromPlayer2AndPlayer2Pulls2AcesFromPlayer1()
        {
            Player1 p1 = new Player1();
            p1.Hand = new List<Card>
            {
                new Card(Suits.Hjärter, Values.Ess),
                new Card(Suits.Hjärter, Values.Kung),
                new Card(Suits.Hjärter, Values.Dam),
                new Card(Suits.Hjärter, Values.Knekt),
                new Card(Suits.Hjärter, Values.Tio)
            };

            Player2 p2 = new Player2();
            p2.Hand = new List<Card>
            {
                new Card(Suits.Spader, Values.Ess),
                new Card(Suits.Spader, Values.Kung),
                new Card(Suits.Spader, Values.Dam),
                new Card(Suits.Spader, Values.Knekt),
                new Card(Suits.Spader, Values.Tio)
            };

            p1.Opponents = new List<BasePlayer> { p2 };
            p2.Opponents = new List<BasePlayer> { p1 };

            p1.CardExchangeAnnouncement += p2.OtherPlayersPlayed;
            p2.CardExchangeAnnouncement += p1.OtherPlayersPlayed;

            var p1Recieved = p2.GetCards(Values.Ess);
            p1.CardExchangeAnnouncement?.Invoke(p1, p2, Values.Ess, p1Recieved);
            p2.CardExchangeAnnouncement?.Invoke(p1, p2, Values.Ess, p1Recieved);

            var expectedHandCountp1 = 6;
            var expectedHandCountp2 = 4;
            var expectedKnownp1 = 1;
            var expectedKnownp2 = 0;

            Assert.AreEqual(expectedHandCountp1, p1.Hand.Count);
            Assert.AreEqual(expectedHandCountp2, p2.Hand.Count);
            Assert.AreEqual(expectedKnownp1, p1.SwappedCards["Player1"].Count);
            Assert.AreEqual(expectedKnownp1, p2.SwappedCards["Player1"].Count);
            Assert.AreEqual(expectedKnownp2, p1.SwappedCards["Player2"].Count);
            Assert.AreEqual(expectedKnownp2, p2.SwappedCards["Player2"].Count);

            var p2Recieved = p1.GetCards(Values.Ess);
            p1.CardExchangeAnnouncement?.Invoke(p2, p1, Values.Ess, p2Recieved);
            p2.CardExchangeAnnouncement?.Invoke(p2, p1, Values.Ess, p2Recieved);

            expectedHandCountp1 = 4;
            expectedHandCountp2 = 6;
            expectedKnownp1 = 0;
            expectedKnownp2 = 2;

            Assert.AreEqual(expectedHandCountp1, p1.Hand.Count);
            Assert.AreEqual(expectedHandCountp2, p2.Hand.Count);
            Assert.AreEqual(expectedKnownp1, p1.SwappedCards["Player1"].Count);
            Assert.AreEqual(expectedKnownp1, p2.SwappedCards["Player1"].Count);
            Assert.AreEqual(expectedKnownp2, p1.SwappedCards["Player2"].Count);
            Assert.AreEqual(expectedKnownp2, p2.SwappedCards["Player2"].Count);
        }
    }
}
