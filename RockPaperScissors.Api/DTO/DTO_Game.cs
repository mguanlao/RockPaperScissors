using System.Collections.Generic;

namespace RockPaperScissors.Api.DTO
{
    public class DTO_Game
    {
        #region Properties
        public List<DTO_GameRound> GameRounds { get; set; }
        public bool HasWinner { get; set; }
        public List<DTO_Player> Players { get; set; }
        #endregion

        #region Constructors
        public DTO_Game()
        {
            GameRounds = new List<DTO_GameRound>();
            Players = new List<DTO_Player>();
            HasWinner = false;
        }

        public DTO_Game(List<DTO_Player> players)
        {
            GameRounds = new List<DTO_GameRound>();
            Players = players;
            HasWinner = false;
        }
        #endregion
    }
}
