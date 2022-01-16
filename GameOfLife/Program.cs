using GameOfLife;

static void Step()
{
    var grid = new Grid();

    var firstCell = grid.Cells.FirstOrDefault(c => c.Index == 9999);

    foreach(var neighbour in firstCell.Neighbours.Values)
    {
        Console.WriteLine(neighbour.Index);
    }
}

Step();