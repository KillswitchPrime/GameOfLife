using BriansBrain.Core;

namespace BriansBrain.Forms
{
    public partial class BriansBrainGrid : Form
    {
        public BriansBrainGrid()
        {
            InitializeComponent();
            var timer = new System.Windows.Forms.Timer
            {
                Interval = 10 // Milliseconds
            };
            timer.Tick += new EventHandler(Step);
            timer.Start();
        }

        private static readonly int _rowSize = 60;
        private static readonly Grid _grid = new (row: _rowSize, startAlive: 80);
        private static Brush brush = Brushes.Gray;
        private static int cellSize = 0;

        private static int X = 0;
        private static int Y = 0;

        private void BriansBrainGrid_Paint(object sender, PaintEventArgs e)
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

            graphics.Dispose();
        }

        private void Step(object? sender, EventArgs e)
        {
            var graphics = CreateGraphics();

            foreach (var cell in _grid.Cells)
            {
                if (cell.WasChanged)
                {
                    brush = cell.IsAlive switch
                    {
                        StatusEnum.Dead => Brushes.Gray,
                        StatusEnum.Alive => Brushes.Coral,
                        StatusEnum.Dying => Brushes.Aquamarine,
                        _ => Brushes.Gray
                    };

                    graphics.FillRectangle(brush, X + 1, Y + 1, cellSize - 1, cellSize - 1);
                }

                X += cellSize;
                if (X >= (_rowSize * cellSize))
                {
                    X = 0;
                    Y += cellSize;
                }

                cell.CheckNextStepStatus();
            }

            X = 0;
            Y = 0;

            _grid.UpdateCellsStatus();
            graphics.Dispose();
        }
    }
}