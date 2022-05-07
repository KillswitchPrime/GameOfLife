namespace BriansBrain.Core
{
    public class Cell
    {
        internal Cell(StatusEnum isAlive = StatusEnum.Dead, int livingNeighbours = 0, int index = 0)
        {
            Neighbours = new Dictionary<CellEnum, StatusEnum>
            {
                {CellEnum.NorthWest, StatusEnum.Dead},
                {CellEnum.North, StatusEnum.Dead},
                {CellEnum.NorthEast, StatusEnum.Dead},
                {CellEnum.West, StatusEnum.Dead},
                {CellEnum.East, StatusEnum.Dead},
                {CellEnum.SouthWest, StatusEnum.Dead},
                {CellEnum.South, StatusEnum.Dead},
                {CellEnum.SouthEast, StatusEnum.Dead},
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
        internal Dictionary<CellEnum, StatusEnum> Neighbours { get; set; }
        public int Index { get; set; }

        public void CheckNextStepStatus()
        {
            var numberOfLivingNeighbours = 0;

            foreach(var cellStatus in Neighbours.Values)
            {
                if (cellStatus == StatusEnum.Alive)
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
