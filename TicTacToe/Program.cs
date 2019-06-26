using TicTacToe.Player;

namespace TicTacToe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new GameWorkflow(
                new HumanConsolePlayer(),
                new MinMaxComputerPlayer())
                .Play();
        }
    }
}
