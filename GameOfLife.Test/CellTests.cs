using GameOfLife.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameOfLife.Test
{
    public class CellTests
    {
        [Fact]
        internal void UpdateStatus_Valid()
        {
            var grid = new Grid(3, 0);

            var cells = grid.Cells;

            Assert.True(cells.All(c => c.IsAlive == false));

            cells.FirstOrDefault(c => c.Index == 1).IsAlive = true;
            cells.FirstOrDefault(c => c.Index == 2).IsAlive = true;

            cells.FirstOrDefault(c => c.Index == 4).CheckNextStepStatus();
            cells.FirstOrDefault(c => c.Index == 4).UpdateStatus();

            Assert.True(cells.FirstOrDefault(c => c.Index == 4).IsAlive);
        }

        [Fact]
        internal void UpdateStatus_OverpopulationValid()
        {
            var grid = new Grid(3, 0);

            var cells = grid.Cells;

            Assert.True(cells.All(c => c.IsAlive == false));

            cells.FirstOrDefault(c => c.Index == 0).IsAlive = true;
            cells.FirstOrDefault(c => c.Index == 1).IsAlive = true;
            cells.FirstOrDefault(c => c.Index == 2).IsAlive = true;
            cells.FirstOrDefault(c => c.Index == 3).IsAlive = true;

            cells.FirstOrDefault(c => c.Index == 5).IsAlive = true;
            cells.FirstOrDefault(c => c.Index == 6).IsAlive = true;
            cells.FirstOrDefault(c => c.Index == 7).IsAlive = true;
            cells.FirstOrDefault(c => c.Index == 8).IsAlive = true;

            foreach(var cell in cells)
            {
                cell.CheckNextStepStatus();
            }

            foreach(var cell in cells)
            {
                cell.UpdateStatus();
            }

            Assert.True(cells.All(c => c.IsAlive == false));
        }
    }
}
