namespace GameOfLife.Core
{
    public class Cell
    {
        internal Cell(bool isAlive = false, int livingNeighbours = 0, int index = 0)
        {
            IsAlive = isAlive;
            LivingNeighbours = livingNeighbours;
            Index = index;
            WasChanged = false;
        }

        public bool IsAlive { get; set; }
        public bool NextStepIsAlive { get; set; }
        public bool WasChanged { get; set; }
        internal int LivingNeighbours { get; set; }
        public int Index { get; set; }

        public void CheckNextStepStatus(int livingNeighbours)
        {
            LivingNeighbours = livingNeighbours;

            if (IsAlive)
            {
                NextStepIsAlive = livingNeighbours switch
                {
                    >= 4 => false,

                    >= 2 => true,

                    >= 0 => false,

                    _ => false
                };
            }
            else if (IsAlive == false)
            {
                NextStepIsAlive = livingNeighbours == 3;
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
