using GameOfLife.Core;
using GameOfLifeWPF.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace GameOfLifeWPF.Pages
{
    /// <summary>
    /// Interaction logic for GameOfLife.xaml
    /// </summary>
    public partial class GameOfLifePage : Page
    {
        public GameOfLifePage()
        {
            InitializeComponent();
            Loaded += GameOfLifePage_Loaded;
        }

        private static readonly int _rowSize = 100;
        private static readonly SimpleGrid _grid = new(row: _rowSize, startAlive: 10);
        private bool[] newValues = new bool[_grid.Size];
        private static double cellSize = 0;
        private static readonly Binding[] brushBindings = new Binding[2];
        private static readonly Rectangle[] rectangles = new Rectangle[_grid.Size];

        private static double X = 0;
        private static double Y = 0;

        private void GameOfLifePage_Loaded(object sender, RoutedEventArgs e)
        {
            cellSize = ActualHeight / _rowSize;

            GameOfLifeGrid();

            brushBindings[0] = new Binding("BrushColor")
            {
                Source = new CellBrushesBinding(Brushes.Gold)
            };
            brushBindings[1] = new Binding("BrushColor")
            {
                Source = new CellBrushesBinding(Brushes.Gray)
            };

            DrawCells();

            var timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 50) // Milliseconds
            };
            timer.Tick += new EventHandler(Step);
            timer.Start();
        }

        private void GameOfLifeGrid()
        {
            for (int i = 0; i <= _grid.Column; i++)
            {
                var currentPoint = i * cellSize;

                Line horizontalLine = new()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    X1 = currentPoint,
                    X2 = currentPoint,
                    Y1 = 0,
                    Y2 = ActualHeight
                };
                Canvas.Children.Add(horizontalLine);

                if (i <= _rowSize)
                {
                    Line verticalLine = new()
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 1,
                        X1 = 0,
                        X2 = ActualWidth,
                        Y1 = currentPoint,
                        Y2 = currentPoint,
                    };
                    Canvas.Children.Add(verticalLine);
                }
            }
        }

        private void DrawCells()
        {
            for(var i = 0; i < _grid.Size; i++)
            {
                Rectangle rectangle = new()
                {
                    Height = cellSize - 2,
                    Width = cellSize - 2,
                    Fill = _grid.Cells[i] ? Brushes.Gold : Brushes.Gray
                };

                rectangles[i] = rectangle;
                Canvas.Children.Add(rectangle);
                Canvas.SetTop(rectangle, Y + 1);
                Canvas.SetLeft(rectangle, X + 1);
                rectangle.SetBinding(Shape.FillProperty, brushBindings[_grid.Cells[i] ? 0 : 1]);

                X += cellSize;
                if (X >= (_grid.Column * cellSize))
                {
                    X = 0;
                    Y += cellSize;
                }
            }
        }

        private void Step(object? sender, EventArgs e)
        {
            for(var i = 0; i < _grid.Size; i++)
            {
                newValues[i] = _grid.UpdateCellStatus(i);
            }

            for (var i = 0; i < _grid.Size; i++)
            {
                var wasAlive = _grid.Cells[i];
                var isAlive = newValues[i];

                if (isAlive != wasAlive)
                {
                    rectangles[i].SetBinding(Shape.FillProperty, brushBindings[isAlive ? 0 : 1]);
                }
            }

            newValues.AsSpan().CopyTo(_grid.Cells);
        }
    }
}

