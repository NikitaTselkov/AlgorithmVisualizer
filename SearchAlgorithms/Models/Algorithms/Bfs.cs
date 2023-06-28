using Core;
using SearchAlgorithms.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models.Algorithms
{
    public class Bfs : AlgoritmBase
    {
        public Bfs(Cell[,] cells, Cell start) : base(cells, start)
        {
            _array = new Queue<Cell>();
        }

        public override async Task StartSearch()
        {
            var currentCell = _start;

            while (currentCell != null)
            {
                AddCellsToArray(currentCell, AddCellToQueue, IsCanMove);

                if (((Queue<Cell>)_array).TryDequeue(out currentCell))
                {
                    if (currentCell.State == State.Finish)
                    {
                        FindPath(currentCell, _cameFrom.FindNode(currentCell).Parent);
                        break;
                    }

                    if (currentCell.State != State.Start)
                        BoardModel.SetState(currentCell.Row, currentCell.Column, State.Visited);
                }

                await Task.Delay(Deley);
            }
        }

        private void AddCellToQueue(int row, int column)
        {
            ((Queue<Cell>)_array).Enqueue(_cells[row, column]);

            if (_cells[row, column].State != State.Finish)
                BoardModel.SetState(row, column, State.InQueue);
        }
    }
}
