namespace GameOfLife.Core
{
    public class Cell
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

        public bool IsAlive { get; set; }
        internal int LivingNeighbours { get; set; }
        internal Dictionary<CellEnum, Cell?> Neighbours { get; set; }
        public int Index { get; set; }

        public bool UpdateStatus()
        {
            var numberOfLivingNeighbours = 0;

            foreach(var cell in Neighbours.Values)
            {
                if (cell.IsAlive)
                {
                    numberOfLivingNeighbours++;
                }
            }

            LivingNeighbours = numberOfLivingNeighbours;

            return numberOfLivingNeighbours switch
            {
                >= 4 => false,

                >= 2 => true,

                >= 0 => false,

                _ => false
            };
        }
    }
}
