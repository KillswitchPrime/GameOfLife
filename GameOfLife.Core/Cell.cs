﻿namespace GameOfLife.Core
{
    public class Cell
    {
        internal Cell(bool isAlive = false, int livingNeighbours = 0, int index = 0)
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

        public bool IsAlive { get; set; }
        public bool NextStepIsAlive { get; set; }
        public bool WasChanged { get; set; }
        internal int LivingNeighbours { get; set; }
        internal Dictionary<CellEnum, Cell> Neighbours { get; set; }
        public int Index { get; set; }

        public void CheckNextStepStatus()
        {
            var numberOfLivingNeighbours = 0;

            foreach (var cell in Neighbours.Values)
            {
                if (cell.IsAlive)
                {
                    numberOfLivingNeighbours++;
                }
            }

            LivingNeighbours = numberOfLivingNeighbours;

            if (IsAlive)
            {
                NextStepIsAlive = numberOfLivingNeighbours switch
                {
                    >= 4 => false,

                    >= 2 => true,

                    >= 0 => false,

                    _ => false
                };
            }
            else if (IsAlive == false)
            {
                NextStepIsAlive = numberOfLivingNeighbours == 3;
            }
        }

        public void UpdateStatus()
        {
            if (IsAlive != NextStepIsAlive)
            {
                WasChanged = true;
            }

            IsAlive = NextStepIsAlive;
        }
    }
}
