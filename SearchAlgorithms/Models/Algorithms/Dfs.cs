using Core;
using SearchAlgorithms.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models.Algorithms
{
    public class Dfs : AlgoritmBase
    {
        public Dfs(Cell[,] cells, Cell start) : base(cells, start)
        {
            _array = new Stack<Cell>();
        }

        public async Task StartSearch()
        {
            var currentCell = _start;

            while (currentCell != null)
            {
                AddCellsToArray(currentCell, AddCellToStack, IsCanMove);

                if (((Stack<Cell>)_array).TryPop(out currentCell))
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

        private void AddCellToStack(int row, int column)
        {
            ((Stack<Cell>)_array).Push(_cells[row, column]);

            if (_cells[row, column].State != State.Finish)
                BoardModel.SetState(row, column, State.InQueue);
        }

        public new bool IsCanMove(int row, int column)
        {
            return _cells[row, column].State != State.Visited
                && _cells[row, column].State != State.Border
                && _cells[row, column].State != State.Start;
        }

        
    }
}
