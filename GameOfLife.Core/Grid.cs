namespace GameOfLife.Core
{
    public class Grid
    {
       public Grid(int row = 100, int startAlive = 10)
       {
            Cells = new List<Cell>();

            var size = row * row;

            var random = new Random();

            for (int i = 0; i < size; i++)
            {
                var chanceToLive = random.Next(0, 101);

                var isAlive = chanceToLive <= startAlive;

                Cells.Add(new Cell(isAlive: isAlive, index: i));
            }

            for(int i = 0; i < size; ++i)
            {
                var currentCell = Cells[i];

                if (i == 0) // Top left corner
                {
                    currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - 1];
                    currentCell.Neighbours[CellEnum.North] = Cells[size - row];
                    currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - row + 1];
                    currentCell.Neighbours[CellEnum.West] = Cells[row - 1];
                    currentCell.Neighbours[CellEnum.East] = Cells[i + 1];
                    currentCell.Neighbours[CellEnum.SouthWest] = Cells[(row * 2) - 1];
                    currentCell.Neighbours[CellEnum.South] = Cells[row];
                    currentCell.Neighbours[CellEnum.SouthEast] = Cells[row + 1];
                }
                else if(i == (row - 1)) // Top right corner
                {
                    currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - 2];
                    currentCell.Neighbours[CellEnum.North] = Cells[size - 1];
                    currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - row];
                    currentCell.Neighbours[CellEnum.West] = Cells[i - 1];
                    currentCell.Neighbours[CellEnum.East] = Cells[0];
                    currentCell.Neighbours[CellEnum.SouthWest] = Cells[(row * 2) - 2];
                    currentCell.Neighbours[CellEnum.South] = Cells[(row * 2) - 1];
                    currentCell.Neighbours[CellEnum.SouthEast] = Cells[row];
                }
                else if(i == (size - row)) // Bottom left corner
                {
                    currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - row - 1];
                    currentCell.Neighbours[CellEnum.North] = Cells[size - (row * 2)];
                    currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - (row * 2) + 1];
                    currentCell.Neighbours[CellEnum.West] = Cells[size - 1];
                    currentCell.Neighbours[CellEnum.East] = Cells[i + 1];
                    currentCell.Neighbours[CellEnum.SouthWest] = Cells[row - 1];
                    currentCell.Neighbours[CellEnum.South] = Cells[0];
                    currentCell.Neighbours[CellEnum.SouthEast] = Cells[1];
                }
                else if(i == (size - 1)) // Bottom right corner
                {
                    currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - row - 2];
                    currentCell.Neighbours[CellEnum.North] = Cells[size - row - 1];
                    currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - (row * 2)];
                    currentCell.Neighbours[CellEnum.West] = Cells[i - 1];
                    currentCell.Neighbours[CellEnum.East] = Cells[size - row];
                    currentCell.Neighbours[CellEnum.SouthWest] = Cells[row - 2];
                    currentCell.Neighbours[CellEnum.South] = Cells[row - 1];
                    currentCell.Neighbours[CellEnum.SouthEast] = Cells[0];
                }
                else if(i > 0 && i < (row - 1)) // Top row
                {
                    currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - row + (i - 1)];
                    currentCell.Neighbours[CellEnum.North] = Cells[size - row + i];
                    currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - row + (i + 1)];
                    currentCell.Neighbours[CellEnum.West] = Cells[i - 1];
                    currentCell.Neighbours[CellEnum.East] = Cells[i + 1];
                    currentCell.Neighbours[CellEnum.SouthWest] = Cells[row + (i - 1)];
                    currentCell.Neighbours[CellEnum.South] = Cells[row + i];
                    currentCell.Neighbours[CellEnum.SouthEast] = Cells[row + (i + 1)];
                }
                else if(i > (size - row) && i < (size - 1)) // Bottom row
                {
                    currentCell.Neighbours[CellEnum.NorthWest] = Cells[i - row - 1];
                    currentCell.Neighbours[CellEnum.North] = Cells[i - row];
                    currentCell.Neighbours[CellEnum.NorthEast] = Cells[i - row + 1];
                    currentCell.Neighbours[CellEnum.West] = Cells[i - 1];
                    currentCell.Neighbours[CellEnum.East] = Cells[i + 1];
                    currentCell.Neighbours[CellEnum.SouthWest] = Cells[i - (row * (row - 1)) + row - 1];
                    currentCell.Neighbours[CellEnum.South] = Cells[i - (row * (row - 1))];
                    currentCell.Neighbours[CellEnum.SouthEast] = Cells[i - (row * (row - 1)) + 1];
                }
                else if(i % row == 0) // Left column
                {
                    currentCell.Neighbours[CellEnum.NorthWest] = Cells[i - 1];
                    currentCell.Neighbours[CellEnum.North] = Cells[i - row];
                    currentCell.Neighbours[CellEnum.NorthEast] = Cells[i - row + 1];
                    currentCell.Neighbours[CellEnum.West] = Cells[i + (row - 1)];
                    currentCell.Neighbours[CellEnum.East] = Cells[i + 1];
                    currentCell.Neighbours[CellEnum.SouthWest] = Cells[i + (row * 2) - 1];
                    currentCell.Neighbours[CellEnum.South] = Cells[i + row];
                    currentCell.Neighbours[CellEnum.SouthEast] = Cells[i + row + 1];
                }
                else if((i + 1) % row == 0) // Right column
                {
                    currentCell.Neighbours[CellEnum.NorthWest] = Cells[i - row - 1];
                    currentCell.Neighbours[CellEnum.North] = Cells[i - row];
                    currentCell.Neighbours[CellEnum.NorthEast] = Cells[i - (row * 2) + 1];
                    currentCell.Neighbours[CellEnum.West] = Cells[i - 1];
                    currentCell.Neighbours[CellEnum.East] = Cells[i - row + 1];
                    currentCell.Neighbours[CellEnum.SouthWest] = Cells[i + row - 1];
                    currentCell.Neighbours[CellEnum.South] = Cells[i + row];
                    currentCell.Neighbours[CellEnum.SouthEast] = Cells[i + 1];
                }
                else // Every non-edge cell
                {
                    currentCell.Neighbours[CellEnum.NorthWest] = Cells[i - row - 1];
                    currentCell.Neighbours[CellEnum.North] = Cells[i - row];
                    currentCell.Neighbours[CellEnum.NorthEast] = Cells[i - row + 1];
                    currentCell.Neighbours[CellEnum.West] = Cells[i - 1];
                    currentCell.Neighbours[CellEnum.East] = Cells[i + 1];
                    currentCell.Neighbours[CellEnum.SouthWest] = Cells[i + row - 1];
                    currentCell.Neighbours[CellEnum.South] = Cells[i + row];
                    currentCell.Neighbours[CellEnum.SouthEast] = Cells[i + row + 1];
                }
            }
       }

       public List<Cell> Cells { get; set; }
    }
}
