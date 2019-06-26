using TicTacToe.Game;

namespace TicTacToe
{
    public interface IPlayer
    {
        Fields GetNextField(IGameState gameState);
    }
}