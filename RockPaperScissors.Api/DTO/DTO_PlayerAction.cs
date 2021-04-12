using RockPaperScissors.Api.ENUM;
using System.Collections.Generic;

namespace RockPaperScissors.Api.DTO
{
    public class DTO_PlayerAction
    {
        #region Properties
        public PlayerAction PlayerAction { get; set; }
        public List<PlayerAction> LosesAgainst { get; set; }
        #endregion

        #region Constructors
        public DTO_PlayerAction()
        { 
            LosesAgainst = new List<PlayerAction>();
        }

        public DTO_PlayerAction(PlayerAction playerAction , List<PlayerAction> losesAgainst)
        {
            PlayerAction = playerAction;
            LosesAgainst = losesAgainst;
        }
        #endregion
    }
}
