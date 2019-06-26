using System;
using TicTacToe.Game;

namespace TicTacToe
{
    public class GameWorkflow
    {
        private readonly IPlayer player1;

        private readonly IPlayer player2;

        public GameWorkflow(IPlayer player1, IPlayer player2)
        {
            this.player1 = player1 ?? throw new ArgumentNullException(nameof(player1));
            this.player2 = player2 ?? throw new ArgumentNullException(nameof(player2));
        }

        public void Play()
        {
            IGameState gameState = new GameState();

            PrintGameState(gameState);
            Console.WriteLine();

            while (gameState.GameResult == GameResult.NoWinnerYet)
            {
                var currentPlayer = gameState.CurrentPlayer == Game.Player.Player1 ? this.player1 : this.player2;
                gameState = gameState.MakeMove(currentPlayer.GetNextField(gameState));

                Console.WriteLine();
                PrintGameState(gameState);
                Console.WriteLine();
            }

            if (gameState.GameResult == GameResult.Draw)
            {
                Console.WriteLine("DRAW -  NO WINNER");
            }
            else if (gameState.GameResult == GameResult.Player1)
            {
                Console.WriteLine("WINNER: Player 1");
            }
            else if (gameState.GameResult == GameResult.Player2)
            {
                Console.WriteLine("WINNER: Player 2");
            }

            Console.WriteLine("Moves: " + gameState.Moves.Count);
            foreach (var move in gameState.Moves)
            {
                Console.WriteLine($" {move}");
            }
        }

        private static void PrintGameState(IGameState gameState)
        {
            Console.WriteLine($" {GetText(gameState.FieldState[0])} | {GetText(gameState.FieldState[1])} | {GetText(gameState.FieldState[2])} ");
            Console.WriteLine($"---|---|---");
            Console.WriteLine($" {GetText(gameState.FieldState[3])} | {GetText(gameState.FieldState[4])} | {GetText(gameState.FieldState[5])} ");
            Console.WriteLine($"---|---|---");
            Console.WriteLine($" {GetText(gameState.FieldState[6])} | {GetText(gameState.FieldState[7])} | {GetText(gameState.FieldState[8])} ");
        }

        private static string GetText(FieldStates state)
        {
            switch (state)
            {
                case FieldStates.Player1:
                    return "X";
                case FieldStates.Player2:
                    return "O";
                default:
                    return " ";
            }
        }
    }
}
