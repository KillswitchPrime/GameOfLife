namespace GameOfLife
{
    internal class Grid
    {
       internal Grid(int row = 100)
       {
            Cells = new List<Cell>();

            var size = Math.Pow(row, 2);

            for (int i = 0; i < size; i++)
            {
                Cells.Add(new Cell());
            }

            for(int i = 0; i < size; ++i)
            {
                var currentCell = Cells[i];
            }
       }

       internal List<Cell> Cells { get; set; }
    }
}
