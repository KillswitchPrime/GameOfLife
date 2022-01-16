using GameOfLife;

static void Step()
{
    var grid = new Grid();
    Console.WriteLine(grid.Cells.Count);
}

Step();