using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine.Models;

namespace GameEngine.Tests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void GameTurnsOn()
        {
            Game newGame = Engine.StartGame();
            Assert.AreEqual(newGame.IsOn, true);
        }

        [TestMethod]
        public void GameTurnsOff()
        {
            Game newGame = Engine.StartGame();
            newGame.TurnOff();
            Assert.AreEqual(newGame.IsOff, true);
        }

        [TestMethod]
        public void PlayerCanJoinEmpty
    }
}
