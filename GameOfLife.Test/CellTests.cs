using GameOfLife.Core;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GameOfLife.Test
{
    public class CellTests
    {
        [Fact]
        internal void UpdateStatus_Valid()
        {
            Grid grid = new(3, 0);

            List<Cell> cells = grid.Cells;

            Assert.True(cells.All(c => c.IsAlive == false));

            cells.First(c => c.Index == 1).IsAlive = true;
            cells.First(c => c.Index == 2).IsAlive = true;

            cells.First(c => c.Index == 4).CheckNextStepStatus(2);
            cells.First(c => c.Index == 4).UpdateStatus();

            Assert.True(cells.First(c => c.Index == 4).IsAlive);
        }

        [Fact]
        internal void UpdateStatus_OverpopulationValid()
        {
            var grid = new Grid(3, 0);

            var cells = grid.Cells;

            Assert.True(cells.All(c => c.IsAlive == false));

            cells.First(c => c.Index == 0).IsAlive = true;
            cells.First(c => c.Index == 1).IsAlive = true;
            cells.First(c => c.Index == 2).IsAlive = true;
            cells.First(c => c.Index == 3).IsAlive = true;

            cells.First(c => c.Index == 5).IsAlive = true;
            cells.First(c => c.Index == 6).IsAlive = true;
            cells.First(c => c.Index == 7).IsAlive = true;
            cells.First(c => c.Index == 8).IsAlive = true;

            foreach (var cell in cells)
            {
                grid.UpdateCellsStatus();
            }

            foreach (var cell in cells)
            {
                cell.UpdateStatus();
            }

            Assert.True(cells.All(c => c.IsAlive == false));
        }
    }
}
