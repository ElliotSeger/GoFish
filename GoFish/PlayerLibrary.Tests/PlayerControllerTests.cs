using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlayerLibrary.Tests
{
    [TestClass]
    public class PlayerControllerTests
    {
        [TestMethod]
        public void TestPlayerControllerContainsPlayer1()
        {
            // Use a mocked or faked viewmodel since there's no need for
            // input and output

            // Arrange
            PlayerController pc = new PlayerController(new DummyViewModel());
            string searchFor = "Player1";
            bool expected = true;

            // Act
            bool actual = pc.GetPlayers().Contains(searchFor);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPlayerControllerReturnsInstanceOfPlayer1()
        {
            // Use a mocked or faked viewmodel since there's no need for
            // input and output

            // Arrange
            PlayerController pc = new PlayerController(new DummyViewModel());
            string searchFor = "Player1";
            Type expected = typeof(Player1);

            // Act
            Type actual = pc.InstantiatePlayer(searchFor).GetType();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
