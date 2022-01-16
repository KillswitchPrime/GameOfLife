namespace GameOfLife.Core
{
    internal class Cell
    {
        internal Cell(bool isAlive = false, int livingNeighbours = 0, int index = 0)
        {
            Neighbours = new Dictionary<CellEnum, Cell?>
            {
                {CellEnum.NorthWest, null},
                {CellEnum.North, null},
                {CellEnum.NorthEast, null},
                {CellEnum.West, null},
                {CellEnum.East, null},
                {CellEnum.SouthWest, null},
                {CellEnum.South, null},
                {CellEnum.SouthEast, null},
            };

            IsAlive = isAlive;
            LivingNeighbours = livingNeighbours;
            Index = index;
        }

        internal bool IsAlive { get; set; }
        internal int LivingNeighbours { get; set; }
        internal Dictionary<CellEnum, Cell?> Neighbours { get; set; }
        internal int Index { get; set; }

        internal void UpdateStatus()
        {
            var numberOfLivingNeighbours = 0;

            foreach(var cell in Neighbours.Values)
            {
                if (cell.IsAlive)
                {
                    numberOfLivingNeighbours++;
                }
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
            LivingNeighbours = numberOfLivingNeighbours;
        }
    }
}
