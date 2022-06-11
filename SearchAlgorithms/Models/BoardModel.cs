using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models
{
    public static class BoardModel
    {
        public static async Task<Cell[,]> GenerateCellsAsync(int rowsCount, int columnsCount)
        {
            var cells = new Cell[rowsCount, columnsCount];

            await Task.Run(() =>
            {
                Parallel.For(0, rowsCount, (rowId) =>
                {
                    Parallel.For(0, columnsCount, (columnId) =>
                    {
                        cells[rowId, columnId] = new Cell(rowId, columnId);
                    });
                });
            }).ConfigureAwait(false);

            return cells;
        }
    }
}
