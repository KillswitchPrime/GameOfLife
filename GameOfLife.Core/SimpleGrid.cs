namespace GameOfLife.Core
{
    public class SimpleGrid
    {
        public bool[] Cells { get; set; }
        public int Size;
        public int Row;
        public int Column;

        public SimpleGrid(int row = 100, int startAlive = 10)
        {
            Row = row;
            Column = Row * 2;
            Size = Row * Column;
            Cells = new bool[Size];

            var random = new Random();

            for (int i = 0; i < Size; i++)
            {
                var chanceToLive = random.Next(0, 101);

                var isAlive = chanceToLive <= startAlive;

                Cells[i] = isAlive;
            }
        }

        public bool UpdateCellStatus(int index)
        {
            if (index == 0) // Top left corner
            {
                return ChainTopLeftCorner(index);
            }
            else if (index == (Column - 1)) // Top right corner
            {
                return ChainTopRightCorner(index);
            }
            else if (index == (Size - Column)) // Bottom left corner
            {
                return ChainBottomLeftCorner(index);
            }
            else if (index == (Size - 1)) // Bottom right corner
            {
                return ChainBottomRightCorner(index);
            }
            else if (index > 0 && index < (Column - 1)) // Top row
            {
                return ChainTopRow(index);
            }
            else if (index > (Size - Column) && index < (Size - 1)) // Bottom row
            {
                return ChainBottomRow(index);
            }
            else if (index % Column == 0) // Left column
            {
                return ChainLeftColumn(index);
            }
            else if ((index + 1) % Column == 0) // Right column
            {
                return ChainRightColumn(index);
            }
            else // Every non-edge cell
            {
                return ChainNonEdgeCell(index);
            }
        }

        private bool CheckCellStatus(int index, int livingNeighbours)
        {
            if (Cells[index]) // Is alive
            {
                return livingNeighbours switch
                {
                    >= 4 => false,

                    >= 2 => true,

                    >= 0 => false,

                    _ => false
                };
            }
            else
            {
                return livingNeighbours == 3;
            }
        }

        private bool ChainTopLeftCorner(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[Size - 1] ? 1 : 0;
            livingNeighbours += Cells[Size - Column] ? 1 : 0;
            livingNeighbours += Cells[Size - Column + 1] ? 1 : 0;

            livingNeighbours += Cells[Column - 1] ? 1 : 0;
            livingNeighbours += Cells[index + 1] ? 1 : 0;

            livingNeighbours += Cells[(Column * 2) - 1] ? 1 : 0;
            livingNeighbours += Cells[Column] ? 1 : 0;
            livingNeighbours += Cells[Column + 1] ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private bool ChainTopRightCorner(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[Size - 2] ? 1 : 0;
            livingNeighbours += Cells[Size - 1] ? 1 : 0;
            livingNeighbours += Cells[Size - Column] ? 1 : 0;

            livingNeighbours += Cells[index - 1] ? 1 : 0;
            livingNeighbours += Cells[0] ? 1 : 0;

            livingNeighbours += Cells[(Column * 2) - 2] ? 1 : 0;
            livingNeighbours += Cells[(Column * 2) - 1] ? 1 : 0;
            livingNeighbours += Cells[Column] ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private bool ChainBottomLeftCorner(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - 1] ? 1 : 0;
            livingNeighbours += Cells[Size - (Column * 2)] ? 1 : 0;
            livingNeighbours += Cells[Size - (Column * 2) + 1] ? 1 : 0;

            livingNeighbours += Cells[Size - 1] ? 1 : 0;
            livingNeighbours += Cells[index + 1] ? 1 : 0;

            livingNeighbours += Cells[Column - 1] ? 1 : 0;
            livingNeighbours += Cells[0] ? 1 : 0;
            livingNeighbours += Cells[1] ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private bool ChainBottomRightCorner(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[Size - Column - 2] ? 1 : 0;
            livingNeighbours += Cells[Size - Column - 1] ? 1 : 0;
            livingNeighbours += Cells[Size - (Column * 2)] ? 1 : 0;

            livingNeighbours += Cells[index - 1] ? 1 : 0;
            livingNeighbours += Cells[Size - Column] ? 1 : 0;

            livingNeighbours += Cells[Column - 2] ? 1 : 0;
            livingNeighbours += Cells[Column - 1] ? 1 : 0;
            livingNeighbours += Cells[0] ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private bool ChainTopRow(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[Size - Column + (index - 1)] ? 1 : 0;
            livingNeighbours += Cells[Size - Column + index] ? 1 : 0;
            livingNeighbours += Cells[Size - Column + (index + 1)] ? 1 : 0;

            livingNeighbours += Cells[index - 1] ? 1 : 0;
            livingNeighbours += Cells[index + 1] ? 1 : 0;

            livingNeighbours += Cells[Column + (index - 1)] ? 1 : 0;
            livingNeighbours += Cells[Column + index] ? 1 : 0;
            livingNeighbours += Cells[Column + (index + 1)] ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private bool ChainBottomRow(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - Column - 1] ? 1 : 0;
            livingNeighbours += Cells[index - Column] ? 1 : 0;
            livingNeighbours += Cells[index - Column + 1] ? 1 : 0;

            livingNeighbours += Cells[index - 1] ? 1 : 0;
            livingNeighbours += Cells[index + 1] ? 1 : 0;

            livingNeighbours += Cells[index - (Size - Column) - 1] ? 1 : 0;
            livingNeighbours += Cells[index - (Size - Column)] ? 1 : 0;
            livingNeighbours += Cells[index - (Size - Column) + 1] ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private bool ChainLeftColumn(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - 1] ? 1 : 0;
            livingNeighbours += Cells[index - Column] ? 1 : 0;
            livingNeighbours += Cells[index - Column + 1] ? 1 : 0;

            livingNeighbours += Cells[index + Column - 1] ? 1 : 0;
            livingNeighbours += Cells[index + 1] ? 1 : 0;

            livingNeighbours += Cells[index + (Column * 2) - 1] ? 1 : 0;
            livingNeighbours += Cells[index + Column] ? 1 : 0;
            livingNeighbours += Cells[index + Column + 1] ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private bool ChainRightColumn(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - Column - 1] ? 1 : 0;
            livingNeighbours += Cells[index - Column] ? 1 : 0;
            livingNeighbours += Cells[index - (Column * 2) + 1] ? 1 : 0;

            livingNeighbours += Cells[index - 1] ? 1 : 0;
            livingNeighbours += Cells[index - Column + 1] ? 1 : 0;

            livingNeighbours += Cells[index + Column - 1] ? 1 : 0;
            livingNeighbours += Cells[index + Column] ? 1 : 0;
            livingNeighbours += Cells[index + 1] ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private bool ChainNonEdgeCell(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - Column - 1] ? 1 : 0;
            livingNeighbours += Cells[index - Column] ? 1 : 0;
            livingNeighbours += Cells[index - Column + 1] ? 1 : 0;

            livingNeighbours += Cells[index - 1] ? 1 : 0;
            livingNeighbours += Cells[index + 1] ? 1 : 0;

            livingNeighbours += Cells[index + Column - 1] ? 1 : 0;
            livingNeighbours += Cells[index + Column] ? 1 : 0;
            livingNeighbours += Cells[index + Column + 1] ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }
    }
}
