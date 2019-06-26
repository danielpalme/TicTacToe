namespace TicTacToe.Game
{
    public class Move
    {
        public Move(Player player, Fields field)
        {
            this.Player = player;
            this.Field = field;
        }

        public Player Player { get; }

        public Fields Field { get; }

        public override string ToString()
        {
            return $"{this.Player}: {(int)this.Field}";
        }
    }
}