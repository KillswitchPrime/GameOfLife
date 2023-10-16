using BriansBrain;

namespace BriansBrain.Core
{
    public class SimpleGrid
    {
        public StatusEnum[] Cells { get; set; }
        public int Size;
        public int Row;
        public int Column;

        public SimpleGrid(int row = 100, int startAlive = 10)
        {
            Row = row;
            Column = Row * 2;
            Size = Row * Column;
            Cells = new StatusEnum[Size];

            var random = new Random();

            for (int i = 0; i < Size; i++)
            {
                var chanceToLive = random.Next(0, 101);

                var isAlive = chanceToLive <= startAlive ? StatusEnum.Alive : StatusEnum.Dead;

                Cells[i] = isAlive;
            }
        }

        public StatusEnum UpdateCellStatus(int index)
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

        private StatusEnum CheckCellStatus(int index, int livingNeighbours)
        {
            if (Cells[index] == StatusEnum.Dead && livingNeighbours == 2)
            {
                return StatusEnum.Alive;
            }
            else if (Cells[index] == StatusEnum.Alive)
            {
                return StatusEnum.Dying;
            }
            else if (Cells[index] == StatusEnum.Dying)
            {
                return StatusEnum.Dead;
            }
                
            return StatusEnum.Dead;
        }

        private StatusEnum ChainTopLeftCorner(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[Size - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - Column] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - Column + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[(Column * 2) - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Column] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Column + 1] == StatusEnum.Alive ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private StatusEnum ChainTopRightCorner(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[Size - 2] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - Column] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[0] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[(Column * 2) - 2] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[(Column * 2) - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Column] == StatusEnum.Alive ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private StatusEnum ChainBottomLeftCorner(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - (Column * 2)] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - (Column * 2) + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[Size - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[0] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[1] == StatusEnum.Alive ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private StatusEnum ChainBottomRightCorner(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[Size - Column - 2] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - (Column * 2)] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - Column] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[Column - 2] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[0] == StatusEnum.Alive ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private StatusEnum ChainTopRow(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[Size - Column + (index - 1)] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - Column + index] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Size - Column + (index + 1)] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[Column + (index - 1)] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Column + index] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[Column + (index + 1)] == StatusEnum.Alive ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private StatusEnum ChainBottomRow(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - Column] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - Column + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index - (Size - Column) - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - (Size - Column)] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - (Size - Column) + 1] == StatusEnum.Alive ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private StatusEnum ChainLeftColumn(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - Column] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - Column + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index + Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index + (Column * 2) - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + Column] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + Column + 1] == StatusEnum.Alive ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private StatusEnum ChainRightColumn(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - Column] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - (Column * 2) + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - Column + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index + Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + Column] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + 1] == StatusEnum.Alive ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }

        private StatusEnum ChainNonEdgeCell(int index)
        {
            var livingNeighbours = 0;
            livingNeighbours += Cells[index - Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - Column] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index - Column + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + 1] == StatusEnum.Alive ? 1 : 0;

            livingNeighbours += Cells[index + Column - 1] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + Column] == StatusEnum.Alive ? 1 : 0;
            livingNeighbours += Cells[index + Column + 1] == StatusEnum.Alive ? 1 : 0;

            return CheckCellStatus(index, livingNeighbours);
        }
    }
}
