using System;
using System.Collections.Generic;
using TicTacToe.Game;

namespace TicTacToe.Player
{
    public class MinMaxComputerPlayer : IPlayer
    {
        private readonly Random random = new Random();

        public Fields GetNextField(IGameState gameState)
        {
            var followUpResults = this.DetermineMoveResults(gameState.CurrentPlayer, gameState);

            MoveResult bestMoveResult = this.DetermineBestMoveResult(followUpResults);

            Console.WriteLine($"{gameState.CurrentPlayer} makes move {(int)bestMoveResult.Move}");

            return bestMoveResult.Move;
        }

        private List<MoveResult> DetermineMoveResults(Game.Player currentPlayer, IGameState gameState)
        {
            GameResult desiredGameResult = currentPlayer == Game.Player.Player1 ? GameResult.Player1 : GameResult.Player2;

            var result = new List<MoveResult>();

            foreach (var field in gameState.GetEmptyFields())
            {
                var newGameState = gameState.MakeMove(field);

                if (newGameState.GameResult == desiredGameResult)
                {
                    result.Clear();
                    result.Add(new MoveResult(field, MoveResultType.Win));
                    return result;
                }
                else if (newGameState.GameResult == GameResult.NoWinnerYet)
                {
                    var followUpResults = this.DetermineMoveResults(currentPlayer, newGameState);

                    MoveResult relevantMoveResult = null;

                    if (newGameState.CurrentPlayer == currentPlayer)
                    {
                        relevantMoveResult = this.DetermineBestMoveResult(followUpResults);
                    }
                    else
                    {
                        relevantMoveResult = this.DetermineWorstMoveResult(followUpResults);
                    }

                    result.Add(new MoveResult(field, relevantMoveResult.MoveResultType));
                }
                else if (newGameState.GameResult == GameResult.Draw)
                {
                    result.Add(new MoveResult(field, MoveResultType.Draw));
                }
                else
                {
                    result.Add(new MoveResult(field, MoveResultType.Loose));
                }
            }

            return result;
        }

        private MoveResult DetermineBestMoveResult(List<MoveResult> moveResults)
        {
            foreach (var moveResult in moveResults)
            {
                if (moveResult.MoveResultType == MoveResultType.Win)
                {
                    return moveResult;
                }
            }

            foreach (var moveResult in moveResults)
            {
                if (moveResult.MoveResultType == MoveResultType.Draw)
                {
                    return moveResult;
                }
            }

            return moveResults[this.random.Next(0, moveResults.Count - 1)];
        }

        private MoveResult DetermineWorstMoveResult(List<MoveResult> moveResults)
        {
            foreach (var moveResult in moveResults)
            {
                if (moveResult.MoveResultType == MoveResultType.Loose)
                {
                    return moveResult;
                }
            }

            foreach (var moveResult in moveResults)
            {
                if (moveResult.MoveResultType == MoveResultType.Draw)
                {
                    return moveResult;
                }
            }

            return moveResults[this.random.Next(0, moveResults.Count - 1)];
        }

        private enum MoveResultType
        {
            Win,

            Loose,

            Draw
        }

        private class MoveResult
        {
            public MoveResult(Fields move, MoveResultType moveResultType)
            {
                this.Move = move;
                this.MoveResultType = moveResultType;
            }

            public Fields Move { get; }

            public MoveResultType MoveResultType { get; }

            public override string ToString()
            {
                return $"{(int)this.Move}: {this.MoveResultType}";
            }
        }
    }
}
