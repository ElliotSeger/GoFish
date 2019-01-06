using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayerLibrary.Tests
{
    [TestClass]
    public class PlayerControllerTests
    {
        [TestMethod]
        public void TestPlayerControllerReturnsPlayer1()
        {
            PlayerController pc = new PlayerController();
            var searchFor = "Player1";
            var expected = true;
            var actual = pc.GetPlayers().Contains(searchFor);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPlayerControllerReturnsInstanceOfPlayer1()
        {
            PlayerController pc = new PlayerController();
            var searchFor = "Player1";
            var expected = typeof(Player1);
            var actual = pc.InstantiatePlayer(searchFor).GetType();
            Assert.AreEqual(expected, actual);
        }
    }
}
