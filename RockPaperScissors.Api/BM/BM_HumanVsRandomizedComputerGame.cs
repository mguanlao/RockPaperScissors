using RockPaperScissors.Api.BASE.BM;
using RockPaperScissors.Api.DTO;
using RockPaperScissors.Api.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Api.BM
{
    public class BM_HumanVsRandomizedComputerGame : BM_GameBase
    {
        #region Constructors
        public BM_HumanVsRandomizedComputerGame() : base(new List<DTO_Player>
                                            {
                                                new DTO_Player(1, PlayerType.Human),
                                                new DTO_Player(2, PlayerType.ComputerRandom)
                                            })
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines the winner based on the two players' actions then returns the winning player
        /// </summary>
        /// <param name="player1Action"></param>
        /// <param name="player2Action"></param>
        /// <returns>Returns the winning player</returns>
        public override DTO_Player PlayGame(PlayerAction player1Action, PlayerAction player2Action = PlayerAction.None)
        {
            RoundNumber++;
            DTO_GameRound gameRound = new DTO_GameRound();
            var computerAction = DetermineComputerAction();

            SetPlayerActions(Game.Players[0], player1Action, RoundNumber);
            SetPlayerActions(Game.Players[1], computerAction, RoundNumber);

            DTO_Player gameWinner = DetermineWinner(player1Action, computerAction);
            gameRound.RoundWinner.Add(RoundNumber, gameWinner);
            CheckIfAPlayerHasWon();

            return gameWinner;
        }
        #endregion
    }
}
