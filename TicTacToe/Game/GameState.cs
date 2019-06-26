using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Game
{
    public class GameState : IGameState
    {
        private readonly FieldStates[] fieldState;

        private readonly List<Move> moves;

        public GameState()
        {
            this.fieldState = new FieldStates[9];
            this.moves = new List<Move>();
        }

        private GameState(FieldStates[] fieldState, Player currentPlayer, List<Move> moves)
        {
            this.fieldState = fieldState ?? throw new ArgumentNullException(nameof(fieldState));
            this.moves = moves ?? throw new ArgumentNullException(nameof(moves));
            this.CurrentPlayer = currentPlayer;
        }

        public IReadOnlyList<FieldStates> FieldState => this.fieldState;

        public IReadOnlyList<Move> Moves => this.moves;

        public Player CurrentPlayer { get; private set; }

        public GameResult GameResult
        {
            get
            {
                if ((this.fieldState[0] == FieldStates.Player1 && this.fieldState[1] == FieldStates.Player1 && this.fieldState[2] == FieldStates.Player1)
                    || (this.fieldState[3] == FieldStates.Player1 && this.fieldState[4] == FieldStates.Player1 && this.fieldState[5] == FieldStates.Player1)
                    || (this.fieldState[6] == FieldStates.Player1 && this.fieldState[7] == FieldStates.Player1 && this.fieldState[8] == FieldStates.Player1)
                    || (this.fieldState[0] == FieldStates.Player1 && this.fieldState[3] == FieldStates.Player1 && this.fieldState[6] == FieldStates.Player1)
                    || (this.fieldState[1] == FieldStates.Player1 && this.fieldState[4] == FieldStates.Player1 && this.fieldState[7] == FieldStates.Player1)
                    || (this.fieldState[2] == FieldStates.Player1 && this.fieldState[5] == FieldStates.Player1 && this.fieldState[8] == FieldStates.Player1)
                    || (this.fieldState[0] == FieldStates.Player1 && this.fieldState[4] == FieldStates.Player1 && this.fieldState[8] == FieldStates.Player1)
                    || (this.fieldState[6] == FieldStates.Player1 && this.fieldState[4] == FieldStates.Player1 && this.fieldState[2] == FieldStates.Player1))
                {
                    return GameResult.Player1;
                }

                if ((this.fieldState[0] == FieldStates.Player2 && this.fieldState[1] == FieldStates.Player2 && this.fieldState[2] == FieldStates.Player2)
                    || (this.fieldState[3] == FieldStates.Player2 && this.fieldState[4] == FieldStates.Player2 && this.fieldState[5] == FieldStates.Player2)
                    || (this.fieldState[6] == FieldStates.Player2 && this.fieldState[7] == FieldStates.Player2 && this.fieldState[8] == FieldStates.Player2)
                    || (this.fieldState[0] == FieldStates.Player2 && this.fieldState[3] == FieldStates.Player2 && this.fieldState[6] == FieldStates.Player2)
                    || (this.fieldState[1] == FieldStates.Player2 && this.fieldState[4] == FieldStates.Player2 && this.fieldState[7] == FieldStates.Player2)
                    || (this.fieldState[2] == FieldStates.Player2 && this.fieldState[5] == FieldStates.Player2 && this.fieldState[8] == FieldStates.Player2)
                    || (this.fieldState[0] == FieldStates.Player2 && this.fieldState[4] == FieldStates.Player2 && this.fieldState[8] == FieldStates.Player2)
                    || (this.fieldState[6] == FieldStates.Player2 && this.fieldState[4] == FieldStates.Player2 && this.fieldState[2] == FieldStates.Player2))
                {
                    return GameResult.Player2;
                }

                if (this.FieldState.Count(f => f == FieldStates.Empty) == 0)
                {
                    return GameResult.Draw;
                }

                return GameResult.NoWinnerYet;
            }
        }

        public IGameState MakeMove(Fields field)
        {
            int index = (int)field;

            if (this.fieldState[index] != FieldStates.Empty)
            {
                throw new ArgumentException("Field is already in use", nameof(field));
            }

            FieldStates[] newFieldState = new FieldStates[9];
            Array.Copy(this.fieldState, 0, newFieldState, 0, 9);
            newFieldState[index] = this.CurrentPlayer == Player.Player1 ? FieldStates.Player1 : FieldStates.Player2;

            var moves = this.moves.ToList();
            moves.Add(new Move(this.CurrentPlayer, field));

            return new GameState(
                newFieldState,
                this.CurrentPlayer == Player.Player1 ? Player.Player2 : Player.Player1,
                moves);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var gameState = (GameState)obj;

            if (!this.CurrentPlayer.Equals(gameState.CurrentPlayer))
            {
                return false;
            }

            for (int i = 0; i < this.fieldState.Length; i++)
            {
                if (this.fieldState[i] != gameState.fieldState[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            // Overflow is fine
            unchecked
            {
                int result = this.CurrentPlayer.GetHashCode();

                for (int i = 0; i < this.fieldState.Length; i++)
                {
                    result += (result * 37) + this.fieldState[i].GetHashCode();
                }

                return result;
            }
        }
    }
}
