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

        public void UpdateStatus()
        {
            var numberOfLivingNeighbours = 0;

            foreach(var cell in Neighbours.Values)
            {
                if (cell.IsAlive)
                {
                    numberOfLivingNeighbours++;
                }
            }

            if(numberOfLivingNeighbours == 0 || numberOfLivingNeighbours == 1)
            {
                IsAlive = false;
            }
            else if (numberOfLivingNeighbours == 2 || numberOfLivingNeighbours == 3)
            {
                IsAlive = true;
            }
            else if(numberOfLivingNeighbours >= 4){
                IsAlive = false;
            }

            LivingNeighbours = numberOfLivingNeighbours;
        }
    }
}
