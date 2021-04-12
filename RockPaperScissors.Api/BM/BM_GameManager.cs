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
    public class BM_GameManager
    {
        #region Public Methods
        /// <summary>
        /// Returns the appropriate BM handler class based on the passed game mode
        /// </summary>
        /// <param name="gameMode"></param>
        /// <returns></returns>
        public static BM_GameBase StartGame(GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.HumanVsHuman:
                    BM_HumanVsHumanGame humanVsHumanGame = new BM_HumanVsHumanGame();
                    return humanVsHumanGame;

                case GameMode.HumanVsComputer:
                    BM_HumanVsComputerGame humanVsComputerGame = new BM_HumanVsComputerGame();
                    return humanVsComputerGame;

                case GameMode.HumanVsRandomizedComputer:
                    BM_HumanVsRandomizedComputerGame humanVsRandomizedComputerGame = new BM_HumanVsRandomizedComputerGame();
                    return humanVsRandomizedComputerGame;

                default:
                    throw new Exception("Invalid Game Mode");
            }
        }
        #endregion
    }
}
