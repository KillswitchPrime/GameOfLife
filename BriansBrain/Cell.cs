namespace BriansBrain.Core
{
    public class Cell
    {
        internal Cell(StatusEnum isAlive = StatusEnum.Dead, int livingNeighbours = 0, int index = 0)
        {
            Neighbours = new Dictionary<CellEnum, Cell>
            {
                {CellEnum.NorthWest, this},
                {CellEnum.North, this},
                {CellEnum.NorthEast, this},
                {CellEnum.West, this},
                {CellEnum.East, this},
                {CellEnum.SouthWest, this},
                {CellEnum.South, this},
                {CellEnum.SouthEast, this},
            };

            IsAlive = isAlive;
            LivingNeighbours = livingNeighbours;
            Index = index;
            WasChanged = false;
        }

        public StatusEnum IsAlive { get; set; }
        public StatusEnum NextStepIsAlive { get; set; }
        public bool WasChanged { get; set; }
        internal int LivingNeighbours { get; set; }
        internal Dictionary<CellEnum, Cell> Neighbours { get; set; }
        public int Index { get; set; }

        public void CheckNextStepStatus()
        {
            var numberOfLivingNeighbours = 0;

            foreach(var cell in Neighbours.Values)
            {
                if (cell.IsAlive == StatusEnum.Alive)
                {
                    numberOfLivingNeighbours++;
                }
            }

            LivingNeighbours = numberOfLivingNeighbours;

            if (IsAlive == StatusEnum.Dead && numberOfLivingNeighbours == 2)
            {
                NextStepIsAlive = StatusEnum.Alive;
            }
            else if(IsAlive == StatusEnum.Alive)
            {
                NextStepIsAlive = StatusEnum.Dying;
            }
            else if (IsAlive == StatusEnum.Dying)
            {
                NextStepIsAlive= StatusEnum.Dead;
            }
        }

        public void UpdateStatus()
        {
            if(IsAlive == NextStepIsAlive)
            {
                WasChanged = true;
            }

            IsAlive = NextStepIsAlive;
        }
    }
}
