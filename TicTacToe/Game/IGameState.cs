using System.Collections.Generic;

namespace TicTacToe.Game
{
    public interface IGameState
    {
        IReadOnlyList<FieldStates> FieldState { get; }

        IReadOnlyList<Move> Moves { get; }

        Player CurrentPlayer { get; }

        GameResult GameResult { get; }

        IGameState MakeMove(Fields field);
    }
}