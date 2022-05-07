namespace BriansBrain.Core
{
    public class Grid
    {
        private readonly int Size;
        private readonly int Row;
        public List<Cell> Cells { get; set; }

        public Grid(int row = 100, int startAlive = 10)
        {
            Cells = new List<Cell>();

            Row = row;

            Size = row * row;

            var random = new Random();

            for (int i = 0; i < Size; i++)
            {
                var chanceToLive = random.Next(0, 101);

                var isAlive = chanceToLive <= startAlive ? StatusEnum.Alive : StatusEnum.Dead;

                Cells.Add(new Cell(isAlive: isAlive, index: i));
            }

            for (int i = 0; i < Size; ++i)
            {
                var currentCell = Cells[i];

                if (i == 0) // Top left corner
                {
                    ChainTopLeftCorner(currentCell, Size, row, i);
                }
                else if (i == (row - 1)) // Top right corner
                {
                    ChainTopRightCorner(currentCell, Size, row, i);
                }
                else if (i == (Size - row)) // Bottom left corner
                {
                    ChainBottomLeftCorner(currentCell, Size, row, i);
                }
                else if (i == (Size - 1)) // Bottom right corner
                {
                    ChainBottomRightCorner(currentCell, Size, row, i);
                }
                else if (i > 0 && i < (row - 1)) // Top row
                {
                    ChainTopRow(currentCell, Size, row, i);
                }
                else if (i > (Size - row) && i < (Size - 1)) // Bottom row
                {
                    ChainBottomRow(currentCell, row, i);
                }
                else if (i % row == 0) // Left column
                {
                    ChainLeftColumn(currentCell, row, i);
                }
                else if ((i + 1) % row == 0) // Right column
                {
                    ChainRightColumn(currentCell, row, i);
                }
                else // Every non-edge cell
                {
                    ChainNonEdgeCell(currentCell, row, i);
                }
            }
        }

        public void UpdateCellsStatus()
        {
            foreach (var cell in Cells)
            {
                var i = cell.Index;


                if (i == 0) // Top left corner
                {
                    ChainTopLeftCorner(cell, Size, Row, i);
                }
                else if (i == (Row - 1)) // Top right corner
                {
                    ChainTopRightCorner(cell, Size, Row, i);
                }
                else if (i == (Size - Row)) // Bottom left corner
                {
                    ChainBottomLeftCorner(cell, Size, Row, i);
                }
                else if (i == (Size - 1)) // Bottom right corner
                {
                    ChainBottomRightCorner(cell, Size, Row, i);
                }
                else if (i > 0 && i < (Row - 1)) // Top row
                {
                    ChainTopRow(cell, Size, Row, i);
                }
                else if (i > (Size - Row) && i < (Size - 1)) // Bottom row
                {
                    ChainBottomRow(cell, Row, i);
                }
                else if (i % Row == 0) // Left column
                {
                    ChainLeftColumn(cell, Row, i);
                }
                else if ((i + 1) % Row == 0) // Right column
                {
                    ChainRightColumn(cell, Row, i);
                }
                else // Every non-edge cell
                {
                    ChainNonEdgeCell(cell, Row, i);
                }

                cell.UpdateStatus();
            }
        }

        private void ChainTopLeftCorner(Cell currentCell, int size, int row, int i)
        {
            currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - 1].IsAlive;
            currentCell.Neighbours[CellEnum.North] = Cells[size - row].IsAlive;
            currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - row + 1].IsAlive;
            currentCell.Neighbours[CellEnum.West] = Cells[row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.East] = Cells[i + 1].IsAlive;
            currentCell.Neighbours[CellEnum.SouthWest] = Cells[(row * 2) - 1].IsAlive;
            currentCell.Neighbours[CellEnum.South] = Cells[row].IsAlive;
            currentCell.Neighbours[CellEnum.SouthEast] = Cells[row + 1].IsAlive;
        }

        private void ChainTopRightCorner(Cell currentCell, int size, int row, int i)
        {
            currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - 2].IsAlive;
            currentCell.Neighbours[CellEnum.North] = Cells[size - 1].IsAlive;
            currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - row].IsAlive;
            currentCell.Neighbours[CellEnum.West] = Cells[i - 1].IsAlive;
            currentCell.Neighbours[CellEnum.East] = Cells[0].IsAlive;
            currentCell.Neighbours[CellEnum.SouthWest] = Cells[(row * 2) - 2].IsAlive;
            currentCell.Neighbours[CellEnum.South] = Cells[(row * 2) - 1].IsAlive;
            currentCell.Neighbours[CellEnum.SouthEast] = Cells[row].IsAlive;
        }

        private void ChainBottomLeftCorner(Cell currentCell, int size, int row, int i)
        {
            currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.North] = Cells[size - (row * 2)].IsAlive;
            currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - (row * 2) + 1].IsAlive;
            currentCell.Neighbours[CellEnum.West] = Cells[size - 1].IsAlive;
            currentCell.Neighbours[CellEnum.East] = Cells[i + 1].IsAlive;
            currentCell.Neighbours[CellEnum.SouthWest] = Cells[row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.South] = Cells[0].IsAlive;
            currentCell.Neighbours[CellEnum.SouthEast] = Cells[1].IsAlive;
        }

        private void ChainBottomRightCorner(Cell currentCell, int size, int row, int i)
        {
            currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - row - 2].IsAlive;
            currentCell.Neighbours[CellEnum.North] = Cells[size - row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - (row * 2)].IsAlive;
            currentCell.Neighbours[CellEnum.West] = Cells[i - 1].IsAlive;
            currentCell.Neighbours[CellEnum.East] = Cells[size - row].IsAlive;
            currentCell.Neighbours[CellEnum.SouthWest] = Cells[row - 2].IsAlive;
            currentCell.Neighbours[CellEnum.South] = Cells[row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.SouthEast] = Cells[0].IsAlive;
        }

        private void ChainTopRow(Cell currentCell, int size, int row, int i)
        {
            currentCell.Neighbours[CellEnum.NorthWest] = Cells[size - row + (i - 1)].IsAlive;
            currentCell.Neighbours[CellEnum.North] = Cells[size - row + i].IsAlive;
            currentCell.Neighbours[CellEnum.NorthEast] = Cells[size - row + (i + 1)].IsAlive;
            currentCell.Neighbours[CellEnum.West] = Cells[i - 1].IsAlive;
            currentCell.Neighbours[CellEnum.East] = Cells[i + 1].IsAlive;
            currentCell.Neighbours[CellEnum.SouthWest] = Cells[row + (i - 1)].IsAlive;
            currentCell.Neighbours[CellEnum.South] = Cells[row + i].IsAlive;
            currentCell.Neighbours[CellEnum.SouthEast] = Cells[row + (i + 1)].IsAlive;
        }

        private void ChainBottomRow(Cell currentCell, int row, int i)
        {
            currentCell.Neighbours[CellEnum.NorthWest] = Cells[i - row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.North] = Cells[i - row].IsAlive;
            currentCell.Neighbours[CellEnum.NorthEast] = Cells[i - row + 1].IsAlive;
            currentCell.Neighbours[CellEnum.West] = Cells[i - 1].IsAlive;
            currentCell.Neighbours[CellEnum.East] = Cells[i + 1].IsAlive;
            currentCell.Neighbours[CellEnum.SouthWest] = Cells[i - (row * (row - 1)) - 1].IsAlive;
            currentCell.Neighbours[CellEnum.South] = Cells[i - (row * (row - 1))].IsAlive;
            currentCell.Neighbours[CellEnum.SouthEast] = Cells[i - (row * (row - 1)) + 1].IsAlive;
        }

        private void ChainLeftColumn(Cell currentCell, int row, int i)
        {
            currentCell.Neighbours[CellEnum.NorthWest] = Cells[i - 1].IsAlive;
            currentCell.Neighbours[CellEnum.North] = Cells[i - row].IsAlive;
            currentCell.Neighbours[CellEnum.NorthEast] = Cells[i - row + 1].IsAlive;
            currentCell.Neighbours[CellEnum.West] = Cells[i + (row - 1)].IsAlive;
            currentCell.Neighbours[CellEnum.East] = Cells[i + 1].IsAlive;
            currentCell.Neighbours[CellEnum.SouthWest] = Cells[i + (row * 2) - 1].IsAlive;
            currentCell.Neighbours[CellEnum.South] = Cells[i + row].IsAlive;
            currentCell.Neighbours[CellEnum.SouthEast] = Cells[i + row + 1].IsAlive;
        }

        private void ChainRightColumn(Cell currentCell, int row, int i)
        {
            currentCell.Neighbours[CellEnum.NorthWest] = Cells[i - row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.North] = Cells[i - row].IsAlive;
            currentCell.Neighbours[CellEnum.NorthEast] = Cells[i - (row * 2) + 1].IsAlive;
            currentCell.Neighbours[CellEnum.West] = Cells[i - 1].IsAlive;
            currentCell.Neighbours[CellEnum.East] = Cells[i - row + 1].IsAlive;
            currentCell.Neighbours[CellEnum.SouthWest] = Cells[i + row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.South] = Cells[i + row].IsAlive;
            currentCell.Neighbours[CellEnum.SouthEast] = Cells[i + 1].IsAlive;
        }

        private void ChainNonEdgeCell(Cell currentCell, int row, int i)
        {
            currentCell.Neighbours[CellEnum.NorthWest] = Cells[i - row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.North] = Cells[i - row].IsAlive;
            currentCell.Neighbours[CellEnum.NorthEast] = Cells[i - row + 1].IsAlive;
            currentCell.Neighbours[CellEnum.West] = Cells[i - 1].IsAlive;
            currentCell.Neighbours[CellEnum.East] = Cells[i + 1].IsAlive;
            currentCell.Neighbours[CellEnum.SouthWest] = Cells[i + row - 1].IsAlive;
            currentCell.Neighbours[CellEnum.South] = Cells[i + row].IsAlive;
            currentCell.Neighbours[CellEnum.SouthEast] = Cells[i + row + 1].IsAlive;
        }
    }
}
