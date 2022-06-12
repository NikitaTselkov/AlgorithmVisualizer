using SearchAlgorithms.Enums;
using System.Threading.Tasks;

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

        public static State GetState(int row, int column)
        {
            return _cells[row, column].State;
        }
        public static void SetState(int row, int column, State state)
        {
            _cells[row, column].State = state;
        }

        public static Cell[,] GetCells()
        {
            return _cells;
        }
    }
}
