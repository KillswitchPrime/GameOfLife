namespace GameOfLife
{
    internal class Grid
    {
        internal Grid(Cell[][] board)
        {
            Board = board;
        }

        internal Cell[][] Board { get; set; }
    }
}
