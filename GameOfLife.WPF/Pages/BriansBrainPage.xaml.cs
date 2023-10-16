using BriansBrain;
using BriansBrain.Core;
using GameOfLifeWPF.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLifeWPF.Pages
{
    /// <summary>
    /// Interaction logic for BriansBrain.xaml
    /// </summary>
    public partial class BriansBrainPage : Page
    {
        public BriansBrainPage()
        {
            InitializeComponent();
            Loaded += GameOfLifePage_Loaded;
        }

        private static readonly int _rowSize = 50;
        private static readonly SimpleGrid _grid = new(row: _rowSize, startAlive: 10);
        private StatusEnum[] newValues = new StatusEnum[_grid.Size];
        private static double cellSize = 0;
        private static readonly Binding[] brushBindings = new Binding[3];
        private static readonly Rectangle[] rectangles = new Rectangle[_grid.Size];

        private static double X = 0;
        private static double Y = 0;

        private void GameOfLifePage_Loaded(object sender, RoutedEventArgs e)
        {
            cellSize = ActualHeight / _rowSize;

            BriansBrainGrid();

            brushBindings[0] = new Binding("BrushColor")
            {
                Source = new CellBrushesBinding(Brushes.Coral)
            };
            brushBindings[1] = new Binding("BrushColor")
            {
                Source = new CellBrushesBinding(Brushes.Aquamarine)
            };
            brushBindings[2] = new Binding("BrushColor")
            {
                Source = new CellBrushesBinding(Brushes.Gray)
            };

            DrawCells();

            var timer = new System.Windows.Threading.DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 50) // Milliseconds
            };
            timer.Tick += new EventHandler(Step);
            timer.Start();
        }

        private void BriansBrainGrid()
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
            for (var i = 0; i < _grid.Size; i++)
            {
                Rectangle rectangle = new()
                {
                    Height = cellSize - 2,
                    Width = cellSize - 2,
                    Fill = _grid.Cells[i] switch
                    {
                        StatusEnum.Alive => Brushes.Coral,
                        StatusEnum.Dying => Brushes.Aquamarine,
                        StatusEnum.Dead => Brushes.Gray,
                        _ => Brushes.Gray
                    }
                };

                rectangles[i] = rectangle;
                Canvas.Children.Add(rectangle);
                Canvas.SetTop(rectangle, Y + 1);
                Canvas.SetLeft(rectangle, X + 1);
                rectangle.SetBinding(Shape.FillProperty, brushBindings[_grid.Cells[i] switch
                {
                    StatusEnum.Alive => 0,
                    StatusEnum.Dying => 1,
                    StatusEnum.Dead => 2,
                    _ => 2
                }]);

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
            for (var i = 0; i < _grid.Size; i++)
            {
                newValues[i] = _grid.UpdateCellStatus(i);
            }

            for (var i = 0; i < _grid.Size; i++)
            {
                var wasAlive = _grid.Cells[i];
                var isAlive = newValues[i];

                if (isAlive != wasAlive)
                {
                    rectangles[i].SetBinding(Shape.FillProperty, brushBindings[isAlive switch
                    {
                        StatusEnum.Alive => 0,
                        StatusEnum.Dying => 1,
                        StatusEnum.Dead => 2,
                        _ => 2
                    }]);
                }
            }

            newValues.AsSpan().CopyTo(_grid.Cells);
        }
    }
}
