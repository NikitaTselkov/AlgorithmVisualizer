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
        public static async Task<DataTable> GenerateCellsAsync(int columnsCount, int rowsCount)
        {
            var dataTable = new DataTable();

            await Task.Run(() => GenerateColumns(dataTable, columnsCount)).ConfigureAwait(false);
            await Task.Run(() => GenerateRows(dataTable, rowsCount)).ConfigureAwait(false);

            return dataTable;
        }

        private static void GenerateColumns(DataTable dataTable, int columnsCount)
        {
            var columns = new DataColumn[columnsCount];

            Parallel.For(0, columnsCount, (columnId) =>
            {
                DataColumn column = new();

                column.ColumnName = columnId.ToString();
                column.DataType = Type.GetType("System.Int32");
                columns[columnId] = column;
            });

            dataTable.Columns.AddRange(columns);
        }

        private static void GenerateRows(DataTable dataTable, int rowsCount)
        {
            var rows = new int[rowsCount];

            Parallel.For(0, rowsCount, (rowId) =>
            {
                rows[rowId] = rowId;
            });

            for (int i = 0; i < rowsCount; i++)
            {
                dataTable.Rows.Add(rows[i]);
            }
        }
    }
}
