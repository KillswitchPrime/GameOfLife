using GameOfLife.Core;

namespace GameOfLife.Forms
{
    public partial class GameOfLifeGrid : Form
    {
        public GameOfLifeGrid()
        {
            InitializeComponent();
        }

        private void GameOfLifeGrid_Paint(object sender, PaintEventArgs e)
        {
            using (var graphics = e.Graphics)
            {
                graphics.Clear(Color.Gray);

                var rowSize = 100;
                var cellSize = Height / rowSize;

                var grid = new Grid(row: rowSize, startAlive: 5);

                for(int i = 0; i < rowSize; i++)
                {
                    var currentPoint = i * cellSize;

                    graphics.DrawLine(Pens.Black, new Point(currentPoint, 0), new Point(currentPoint, Height));
                    graphics.DrawLine(Pens.Black, new Point(0, currentPoint), new Point(Width, currentPoint));
                }

                Step(grid, cellSize, rowSize, graphics);
            };
        }

        private void Step(Grid grid, int cellSize, int rowSize, Graphics graphics)
        {
            var x = 0;
            var y = 0;

            var gridCopy = grid;

            foreach(var cell in grid.Cells)
            {
                if (cell.IsAlive)
                {
                    graphics.FillRectangle(Brushes.Gold, new Rectangle(x + 1, y + 1, cellSize - 1, cellSize - 1));
                }
                else
                {
                    graphics.FillRectangle(Brushes.Gray, new Rectangle(x + 1, y + 1, cellSize - 1, cellSize - 1));
                }

                gridCopy.Cells.FirstOrDefault(c => c.Index == cell.Index).UpdateStatus();

                x += cellSize;
                if(x >= (rowSize * cellSize))
                {
                    x = 0;
                    y += cellSize;
                }
            }

            Step(gridCopy, cellSize, rowSize, graphics);
        }
    }
}