using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Game;

namespace TicTacToe.Player
{
    public class RandomComputerPlayer : IPlayer
    {
        private readonly Random random = new Random();

        public Fields GetNextField(IGameState gameState)
        {
            var fields = gameState.GetEmptyFields().ToList();

            return fields[this.random.Next(0, fields.Count - 1)];
        }
    }
}
