using SearchAlgorithms.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models.Algorithms
{
    public class AStar : AlgoritmBase
    {
        public AStar(Cell[,] cells, Cell start) : base(cells, start)
        {
            _array = new List<Cell>();
        }

        public override async Task StartSearch()
        {
            var currentCell = _start;

            while (currentCell != null)
            {
                AddCellsToArray(currentCell, AddCellToQueue, IsCanMove);

                currentCell = ((List<Cell>)_array).Where(s => s.State != State.Visited).Min(m => m);

                if (currentCell.State == State.Finish)
                {
                    FindPath(currentCell, _cameFrom.FindNode(currentCell).Parent);
                    break;
                }

                if (currentCell.State != State.Start)
                    BoardModel.SetState(currentCell.Row, currentCell.Column, State.Visited);

                await Task.Delay(Deley);
            }
        }

        private void AddCellToQueue(int row, int column)
        {
            ((List<Cell>)_array).Add(_cells[row, column]);

            _cells[row, column].Weight = BoardModel.GetDistanceToFinish(_cells[row, column]);

            if (_cells[row, column].State != State.Finish)
                BoardModel.SetState(row, column, State.InQueue);
        }
    }
}
