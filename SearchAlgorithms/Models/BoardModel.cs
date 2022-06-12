using Core;
using SearchAlgorithms.Enums;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SearchAlgorithms.Models
{
    public static class BoardModel
    {
        private static Cell[,] _cells;

        public static async Task GenerateCellsAsync(int rowsCount, int columnsCount)
        {
            _cells = new Cell[rowsCount, columnsCount];

            await Task.Run(() =>
            {
                Parallel.For(0, rowsCount, (rowId) =>
                {
                    Parallel.For(0, columnsCount, (columnId) =>
                    {
                        _cells[rowId, columnId] = new Cell(rowId, columnId);
                    });
                });
            }).ConfigureAwait(false);
        }

        public static Cell[,] GetCells()
        {
            return _cells;
        }

        public static void PaintCells(Cell[,] cells, DataGrid dataGrid)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    PaintCell(cells[i, j], dataGrid);
                }
            }
        }

        public static void PaintCell(Cell cell, DataGrid dataGrid)
        {
            DataGridCell dataGridCell = DataGridHelper.GetCell(dataGrid, cell.Row, cell.Column);
            if (cell != null && dataGridCell != null)
            {
                switch (cell.State)
                {
                    case State.Null:
                        dataGridCell.Background = Brushes.White;
                        break;
                    case State.Border:
                        dataGridCell.Background = Brushes.Black;
                        break;
                    case State.Visited:
                        dataGridCell.Background = Brushes.LightBlue;
                        break;
                    case State.Path:
                        dataGridCell.Background = Brushes.Yellow;
                        break;
                    case State.Start:
                        dataGridCell.Background = Brushes.Orange;
                        break;
                    case State.Finish:
                        dataGridCell.Background = Brushes.Purple;
                        break;
                }
            }
        }

        public static State GetState(int row, int column)
        {
            return _cells[row, column].State;
        }

        public static void SetState(int row, int column, State state)
        {
            _cells[row, column].State = state;
        }
    }
}
