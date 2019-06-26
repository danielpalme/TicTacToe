using System;
using TicTacToe.Game;

namespace TicTacToe.Player
{
    public class HumanConsolePlayer : IPlayer
    {
        public Fields GetNextField(IGameState gameState)
        {
            Console.WriteLine($"{gameState.CurrentPlayer} enter your move:");

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int field))
                {
                    var emptyFields = gameState.GetEmptyIndices();

                    if (emptyFields.Contains(field))
                    {
                        return (Fields)field;
                    }
                    else
                    {
                        Console.WriteLine($"{gameState.CurrentPlayer} enter one of the following numbers: {string.Join(", ", emptyFields)}:");
                    }
                }
                else
                {
                    Console.WriteLine($"{gameState.CurrentPlayer} enter a valid number:");
                }
            }
        }
    }
}
