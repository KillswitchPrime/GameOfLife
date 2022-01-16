namespace GameOfLife
{
    internal class Cell
    {
        internal bool IsAlive { get; set; }
        internal int Neighbours { get; set; } // Living

        // Neighbouring cells
        internal Cell TopLeft { get; set; }
        internal Cell Top { get; set; }
        internal Cell TopRight { get; set; }

        internal Cell Left { get; set; }
        internal Cell Right { get; set; }

        internal Cell BottomLeft { get; set; }
        internal Cell Bottom { get; set; }
        internal Cell BottomRight { get; set; }

        internal void UpdateStatus()
        {
            var numberOfLivingNeighbours = 0;

            if (TopLeft.IsAlive)
            {
                numberOfLivingNeighbours++;
            }
            if (Top.IsAlive)
            {
                numberOfLivingNeighbours++;
            }
            if (TopRight.IsAlive)
            {
                numberOfLivingNeighbours++;
            }
            if (Left.IsAlive)
            {
                numberOfLivingNeighbours++;
            }
            if (Right.IsAlive)
            {
                numberOfLivingNeighbours++;
            }
            if (BottomLeft.IsAlive)
            {
                numberOfLivingNeighbours++;
            }
            if (Bottom.IsAlive)
            {
                numberOfLivingNeighbours++;
            }
            if (BottomRight.IsAlive)
            {
                numberOfLivingNeighbours++;
            }

            var isAlive = numberOfLivingNeighbours switch
            {
                // Equal to 0 or 1
                >= 0 and <= 1 => false,

                // Equal to 2 or 3
                >= 2 and <= 3 => true,

                // Greater than 4
                >= 4 => false,

                _ => false,
            };

            IsAlive = isAlive;
            Neighbours = numberOfLivingNeighbours;
        }
    }
}
