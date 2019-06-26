using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Game
{
    public static class GameStateExtensions
    {
        public static HashSet<int> GetEmptyIndices(this IGameState gameState)
        {
            var emptyFields = Enumerable.Range(0, gameState.FieldState.Count)
                        .Where(i => gameState.FieldState[i] == FieldStates.Empty)
                        .ToHashSet();

            return emptyFields;
        }

        public static IEnumerable<Fields> GetEmptyFields(this IGameState gameState)
        {
            var emptyFields = Enumerable.Range(0, gameState.FieldState.Count)
                        .Where(i => gameState.FieldState[i] == FieldStates.Empty)
                        .Select(i => (Fields)i);

            return emptyFields;
        }
    }
}
