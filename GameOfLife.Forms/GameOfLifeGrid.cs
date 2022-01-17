using GameOfLife.Core;

namespace GameOfLife.Forms
{
    public partial class GameOfLifeGrid : Form
    {
        public GameOfLifeGrid()
        {
            InitializeComponent();
            var timer = new System.Windows.Forms.Timer
            {
                Interval = 200 // Milliseconds
            };
            timer.Tick += new EventHandler(Step);
            timer.Start();
        }

        private static readonly int _rowSize = 100;
        private static Grid _grid = new Grid(row: _rowSize, startAlive: 5);
        private static Brush brush = Brushes.Gray;
        private static int cellSize = 0;

        private static int X = 0;
        private static int Y = 0;

        private void GameOfLifeGrid_Paint(object sender, PaintEventArgs e)
        {
            using var graphics = e.Graphics;
            graphics.Clear(Color.Gray);

            cellSize = Height / _rowSize;

            for (int i = 0; i < _rowSize; i++)
            {
                var currentPoint = i * cellSize;

                graphics.DrawLine(Pens.Black, new Point(currentPoint, 0), new Point(currentPoint, Height));
                graphics.DrawLine(Pens.Black, new Point(0, currentPoint), new Point(Width, currentPoint));
            }
        }

        private void Step(object sender, EventArgs e)
        {
            var graphics = CreateGraphics();

            var gridCopy = _grid;

            foreach (var cell in _grid.Cells)
            {
                brush = cell.IsAlive ? Brushes.Gold : Brushes.Gray;

                graphics.FillRectangle(brush, new Rectangle(X + 1, Y + 1, cellSize - 1, cellSize - 1));

                X += cellSize;
                if (X >= (_rowSize * cellSize))
                {
                    X = 0;
                    Y += cellSize;
                }

                gridCopy.Cells[cell.Index].IsAlive = cell.UpdateStatus();
            }

            X = 0;
            Y = 0;
            _grid = gridCopy;
        }
    }
}