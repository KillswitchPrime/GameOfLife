using GameOfLifeWPF.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

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

        private static readonly int _rowSize = 200;
        private static readonly GameOfLife.Core.Grid _grid = new(row: _rowSize, startAlive: 10);
        private static SolidColorBrush brush = Brushes.Black;
        private static double cellSize = 0;
        private static readonly List<CellBrushesBinding> cellBrushes = new();

        private static double X = 0;
        private static double Y = 0;

        private void GameOfLifePage_Loaded(object sender, RoutedEventArgs e)
        {
            cellSize = ActualHeight / _rowSize;

            GameOfLifeGrid();
            DrawCells();

            var timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 10) // Milliseconds
            };
            timer.Tick += new EventHandler(Step);
            timer.Start();
        }

        private void GameOfLifeGrid()
        {
            for (int i = 0; i < _rowSize; i++)
            {
                var currentPoint = i * cellSize;


                Line horizontalLine = new()
                {
                    Stroke = brush,
                    StrokeThickness = 1,
                    X1 = currentPoint,
                    X2 = currentPoint,
                    Y1 = 0,
                    Y2 = ActualHeight
                };
                Canvas.Children.Add(horizontalLine);

                Line verticalLine = new()
                {
                    Stroke = brush,
                    StrokeThickness = 1,
                    X1 = 0,
                    X2 = ActualWidth,
                    Y1 = currentPoint,
                    Y2 = currentPoint,
                };
                Canvas.Children.Add(verticalLine);
            }
        }

        private void DrawCells()
        {
            foreach (var cell in _grid.Cells)
            {
                brush = cell.IsAlive ? Brushes.Gold : Brushes.Gray;

                Rectangle rectangle = new()
                {
                    Height = cellSize - 2,
                    Width = cellSize - 2,
                    Fill = brush
                };

                Canvas.Children.Add(rectangle);
                Canvas.SetTop(rectangle, Y + 1);
                Canvas.SetLeft(rectangle, X + 1);

                // Binding color property;
                var cellBrushBinding = new CellBrushesBinding(brush);
                cellBrushes.Add(cellBrushBinding);

                var binding = new Binding("BrushColor")
                {
                    Source = cellBrushBinding
                };
                rectangle.SetBinding(Shape.FillProperty, binding);

                X += cellSize;
                if (X >= (_rowSize * cellSize))
                {
                    X = 0;
                    Y += cellSize;
                }
            }
        }

        private void Step(object sender, EventArgs e)
        {
            foreach (var cell in _grid.Cells)
            {
                if (cell.WasChanged)
                {
                    brush = cell.IsAlive ? Brushes.Gold : Brushes.Gray;

                    cellBrushes[cell.Index].BrushColor = brush;
                }

                cell.CheckNextStepStatus();
            }

            _grid.UpdateCellsStatus();
        }
    }
}

