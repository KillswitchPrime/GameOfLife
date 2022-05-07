namespace GameOfLife.Core
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

                var isAlive = chanceToLive <= startAlive;

                Cells.Add(new Cell(isAlive: isAlive, index: i));
            }

            UpdateCellsStatus();
        }

        public void UpdateCellsStatus()
        {
            foreach(var cell in Cells)
            {
                var i = cell.Index;
                

                if (i == 0) // Top left corner
                {
                    cell.CheckNextStepStatus(TopLeftCornerCheckNeighbourStatus(Size, Row, i));
                }
                else if (i == (Row - 1)) // Top right corner
                {
                    cell.CheckNextStepStatus(TopRightCornerCheckNeighbourStatus(Size, Row, i));
                }
                else if (i == (Size - Row)) // Bottom left corner
                {
                    cell.CheckNextStepStatus(BottomLeftCornerCheckNeighbourStatus(Size, Row, i));
                }
                else if (i == (Size - 1)) // Bottom right corner
                {
                    cell.CheckNextStepStatus(BottomRightCornerCheckNeighbourStatus(Size, Row, i));
                }
                else if (i > 0 && i < (Row - 1)) // Top row
                {
                    cell.CheckNextStepStatus(TopRowCheckNeighbourStatus(Size, Row, i));
                }
                else if (i > (Size - Row) && i < (Size - 1)) // Bottom row
                {
                    cell.CheckNextStepStatus(BottomRowCheckNeighbourStatus(Row, i));
                }
                else if (i % Row == 0) // Left column
                {
                    cell.CheckNextStepStatus(LeftColumnCheckNeighbourStatus(Row, i));
                }
                else if ((i + 1) % Row == 0) // Right column
                {
                    cell.CheckNextStepStatus(RightColumnCheckNeighbourStatus(Row, i));
                }
                else // Every non-edge cell
                {
                    cell.CheckNextStepStatus(NonEdgeCellCheckNeighbourStatus(Row, i));
                }

                cell.UpdateStatus();
            }
        }

        private int TopLeftCornerCheckNeighbourStatus(int size, int row, int i)
        {
            var livingNeighbours = 0;

            livingNeighbours += Cells[size - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - row + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[(row * 2) - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row + 1].IsAlive ? 1 : 0;

            return livingNeighbours;
        }

        private int TopRightCornerCheckNeighbourStatus(int size, int row, int i)
        {
            var livingNeighbours = 0;

            livingNeighbours += Cells[size - 2].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[0].IsAlive ? 1 : 0;
            livingNeighbours += Cells[(row * 2) - 2].IsAlive ? 1 : 0;
            livingNeighbours += Cells[(row * 2) - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row].IsAlive ? 1 : 0;

            return livingNeighbours;
        }

        private int BottomLeftCornerCheckNeighbourStatus(int size, int row, int i)
        {
            var livingNeighbours = 0;

            livingNeighbours += Cells[size - row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - (row * 2)].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - (row * 2) + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[0].IsAlive ? 1 : 0;
            livingNeighbours += Cells[1].IsAlive ? 1 : 0;

            return livingNeighbours;
        }

        private int BottomRightCornerCheckNeighbourStatus(int size, int row, int i)
        {
            var livingNeighbours = 0;

            livingNeighbours += Cells[size - row - 2].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - (row * 2)].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row - 2].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[0].IsAlive ? 1 : 0;

            return livingNeighbours;
        }

        private int TopRowCheckNeighbourStatus(int size, int row, int i)
        {
            var livingNeighbours = 0;

            livingNeighbours += Cells[size - row + (i - 1)].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - row + i].IsAlive ? 1 : 0;
            livingNeighbours += Cells[size - row + (i + 1)].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row + (i - 1)].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row + i].IsAlive ? 1 : 0;
            livingNeighbours += Cells[row + (i + 1)].IsAlive ? 1 : 0;

            return livingNeighbours;
        }

        private int BottomRowCheckNeighbourStatus(int row, int i)
        {
            var livingNeighbours = 0;

            livingNeighbours += Cells[i - row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - row + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - (row * (row - 1)) - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - (row * (row - 1))].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - (row * (row - 1)) + 1].IsAlive ? 1 : 0;

            return livingNeighbours;
        }

        private int LeftColumnCheckNeighbourStatus(int row, int i)
        {
            var livingNeighbours = 0;

            livingNeighbours += Cells[i - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - row + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + (row - 1)].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + (row * 2) - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + row + 1].IsAlive ? 1 : 0;

            return livingNeighbours;
        }

        private int RightColumnCheckNeighbourStatus(int row, int i)
        {
            var livingNeighbours = 0;

            livingNeighbours += Cells[i - row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - (row * 2) + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - row + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + 1].IsAlive ? 1 : 0;

            return livingNeighbours;
        }

        private int NonEdgeCellCheckNeighbourStatus(int row, int i)
        {
            var livingNeighbours = 0;

            livingNeighbours += Cells[i - row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - row + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + row - 1].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + row].IsAlive ? 1 : 0;
            livingNeighbours += Cells[i + row + 1].IsAlive ? 1 : 0;

            return livingNeighbours;
        }
    }
}
