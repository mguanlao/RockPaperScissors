using Microsoft.VisualStudio.TestTools.UnitTesting;
using RockPaperScissors.Api.BASE.BM;
using RockPaperScissors.Api.BM;
using RockPaperScissors.Api.ENUM;
using System;

namespace RockPaperScissors.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void StartGameShoudlReturnHumanVsHuman()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsHuman);

            Assert.IsTrue(game.GetType() == typeof(BM_HumanVsHumanGame));
        }

        [TestMethod]
        public void StartGameShoudlReturnHumanVsComputer()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsComputer);

            Assert.IsTrue(game.GetType() == typeof(BM_HumanVsComputerGame));
        }

        [TestMethod]
        public void StartGameShoudlReturnHumanVsRandomizedComputer()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsRandomizedComputer);

            Assert.IsTrue(game.GetType() == typeof(BM_HumanVsRandomizedComputerGame));
        }

        [TestMethod]
        public void HumanVsHumanPlayGameShouldAddRound()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsHuman);

            game.PlayGame(PlayerAction.Rock, PlayerAction.Rock);

            Assert.IsTrue(game.RoundNumber == 1);

            game.PlayGame(PlayerAction.Rock, PlayerAction.Rock);

            Assert.IsTrue(game.RoundNumber == 2);
        }
        
        [TestMethod]
        public void HumanVsHumanPlayGameSetPlayerAction()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsHuman);

            game.SetPlayerActions(game.Game.Players[0], PlayerAction.Rock, 1);

            Assert.IsTrue(game.Game.Players[0].PlayerActions[1] == PlayerAction.Rock);
        }

        [TestMethod]
        public void HumanVsHumanPlayGameShouldResultTie()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsHuman);

            Assert.IsTrue(game.DetermineWinner(PlayerAction.Rock, PlayerAction.Rock) == null);
        }

        [TestMethod]
        public void HumanVsHumanPlayGamePlayer1Wins()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsHuman);

            Assert.IsTrue(game.PlayGame(PlayerAction.Rock, PlayerAction.Scissor).PlayerNumber == 1);
            Assert.IsTrue(game.PlayGame(PlayerAction.Rock, PlayerAction.Flamethrower).PlayerNumber == 1);
            Assert.IsTrue(game.PlayGame(PlayerAction.Scissor, PlayerAction.Paper).PlayerNumber == 1);
            Assert.IsTrue(game.PlayGame(PlayerAction.Scissor, PlayerAction.Flamethrower).PlayerNumber == 1);
            Assert.IsTrue(game.PlayGame(PlayerAction.Paper, PlayerAction.Rock).PlayerNumber == 1);
            Assert.IsTrue(game.PlayGame(PlayerAction.Flamethrower, PlayerAction.Paper).PlayerNumber == 1);
        }

        [TestMethod]
        public void HumanVsHumanPlayGamePlayer2Wins()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsHuman);

            Assert.IsTrue(game.PlayGame(PlayerAction.Rock, PlayerAction.Paper).PlayerNumber == 2);
            Assert.IsTrue(game.PlayGame(PlayerAction.Scissor, PlayerAction.Rock).PlayerNumber == 2);
            Assert.IsTrue(game.PlayGame(PlayerAction.Paper, PlayerAction.Scissor).PlayerNumber == 2);
            Assert.IsTrue(game.PlayGame(PlayerAction.Flamethrower, PlayerAction.Rock).PlayerNumber == 2);
        }

        [TestMethod]
        public void HumanVsHumanPlayGamePlayerWinsAfter3Rounds()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsHuman);

            game.PlayGame(PlayerAction.Rock, PlayerAction.Scissor);
            game.PlayGame(PlayerAction.Rock, PlayerAction.Scissor);
            game.PlayGame(PlayerAction.Rock, PlayerAction.Scissor);

            Assert.IsTrue(game.Game.Players[0].WinCount == 3);
            Assert.IsTrue(game.Game.HasWinner);
        }

        [TestMethod]
        public void HumanVsComputerPlayGameShouldAddRound()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsComputer);

            game.PlayGame(PlayerAction.Rock);

            Assert.IsTrue(game.RoundNumber == 1);

            game.PlayGame(PlayerAction.Rock);

            Assert.IsTrue(game.RoundNumber == 2);
        }

        [TestMethod]
        public void HumanVsComputerRandomizedShouldAddRound()
        {
            BM_GameBase game = BM_GameManager.StartGame(GameMode.HumanVsRandomizedComputer);

            game.PlayGame(PlayerAction.Rock);

            Assert.IsTrue(game.RoundNumber == 1);

            game.PlayGame(PlayerAction.Rock);

            Assert.IsTrue(game.RoundNumber == 2);
        }
    }
}
