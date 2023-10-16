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
                Interval = 50 // Milliseconds
            };
            timer.Tick += new EventHandler(Step);
            timer.Start();
        }

        private static readonly int _rowSize = 100;
        private static readonly SimpleGrid _grid = new(row: _rowSize, startAlive: 10);
        private static readonly bool[] newValues = new bool[_grid.Size];
        private static Brush brush = Brushes.Gray;
        private static int cellSize = 0;

        private static int X = 0;
        private static int Y = 0;

        private void GameOfLifeGrid_Paint(object sender, PaintEventArgs e)
        {
            using var graphics = e.Graphics;
            graphics.Clear(Color.Gray);

            cellSize = Height / _rowSize * 90 / 100;

            for (int i = 0; i <= _grid.Column; i++)
            {
                var currentPoint = i * cellSize;

                // Vertical
                graphics.DrawLine(Pens.Black, new Point(currentPoint, 0), new Point(currentPoint, Height));

                // Horizontal
                if(i <= _rowSize)
                    graphics.DrawLine(Pens.Black, new Point(0, currentPoint), new Point(Width, currentPoint));
            }

            graphics.Dispose();
        }

        private void Step(object? sender, EventArgs e)
        {
            var graphics = CreateGraphics();
            X = 0;
            Y = 0;

            for (var i = 0; i < _grid.Size; i++)
            {
                newValues[i] = _grid.UpdateCellStatus(i);
            }

            for(var i = 0; i < _grid.Size; i++)
            {
                var wasAlive = _grid.Cells[i];
                var isAlive = newValues[i];

                if (isAlive != wasAlive)
                {
                    brush = isAlive ? Brushes.Gold : Brushes.Gray;

                    graphics.FillRectangle(brush, X + 1, Y + 1, cellSize - 1, cellSize - 1);
                }

                X += cellSize;
                if (X >= (_grid.Column * cellSize))
                {
                    X = 0;
                    Y += cellSize;
                }
            }

            newValues.AsSpan().CopyTo(_grid.Cells);

            graphics.Dispose();
        }
    }
}