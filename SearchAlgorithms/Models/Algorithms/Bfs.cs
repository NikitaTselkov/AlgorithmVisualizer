using Core;
using SearchAlgorithms.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models.Algorithms
{
    public class Bfs
    {
        public int Deley { get; set; } = 50;

        private readonly Cell[,] _cells;
        private readonly Cell _start;
        private readonly Queue<Cell> _queue;
        private readonly TreeNode<Cell> _cameFrom;

        public Bfs(Cell[,] cells, Cell start)
        {
            _cells = cells;
            _start = start;
            _queue = new Queue<Cell>();
            _cameFrom = new TreeNode<Cell>(_start);
        }

        public async Task StartSearch()
        {
            var currentCell = _start;

            while (currentCell != null)
            {
                AddCellsToQueue(currentCell);

                if (_queue.TryDequeue(out currentCell))
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

        private void FindPath(Cell finish, TreeNode<Cell> treeNode)
        {
            if (_start != treeNode.Item)
            {
                FindPath(finish, treeNode.Parent);
                BoardModel.SetState(treeNode.Item.Row, treeNode.Item.Column, State.Path);
            }
        }

        private void AddCellsToQueue(Cell cell)
        {
            if (0 <= cell.Row - 1 && IsCanMove(cell.Row - 1, cell.Column))
            {
                AddCellToQueue(cell.Row - 1, cell.Column);
                AddCellToTree(cell, cell.Row - 1, cell.Column);
            }

            if (0 <= cell.Column - 1 && IsCanMove(cell.Row, cell.Column - 1))
            {
                AddCellToQueue(cell.Row, cell.Column - 1);
                AddCellToTree(cell, cell.Row, cell.Column - 1);
            }

            if (_cells.GetLength(0) > cell.Row + 1 && IsCanMove(cell.Row + 1, cell.Column))
            {
                AddCellToQueue(cell.Row + 1, cell.Column);
                AddCellToTree(cell, cell.Row + 1, cell.Column);
            }

            if (_cells.GetLength(1) > cell.Column + 1 && IsCanMove(cell.Row, cell.Column + 1))
            {
                AddCellToQueue(cell.Row, cell.Column + 1);
                AddCellToTree(cell, cell.Row, cell.Column + 1);
            }
        }

        private void AddCellToQueue(int row, int column)
        {
            _queue.Enqueue(_cells[row, column]);

            if (_cells[row, column].State != State.Finish)
                BoardModel.SetState(row, column, State.InQueue);
        }

        private void AddCellToTree(Cell currentCell, int row, int column)
        {
            var currentNode = _cameFrom.FindNode(currentCell);
            currentNode.AddChild(_cells[row, column]);
        }

        private bool IsCanMove(int row, int column)
        {
            return _cells[row, column].State != State.Visited
                && _cells[row, column].State != State.Border
                && _cells[row, column].State != State.Start
                && _cells[row, column].State != State.InQueue;
        }
    }
}
