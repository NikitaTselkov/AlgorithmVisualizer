using Core;
using SearchAlgorithms.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models.Algorithms
{
    public abstract class AlgoritmBase
    {
        private protected IEnumerable<Cell> _array;
        private protected readonly Cell _start;
        private protected readonly TreeNode<Cell> _cameFrom;

        private protected int Deley { get; set; } = 50;
        private protected readonly Cell[,] _cells;


        public AlgoritmBase(Cell[,] cells, Cell start)
        {
            _cells = cells;
            _start = start;
            _cameFrom = new TreeNode<Cell>(start);
        }

        private protected void AddCellsToArray(Cell cell, Action<int, int> addCellToArray, Func<int, int, bool> isCanMove)
        {
            if (0 <= cell.Row - 1 && isCanMove(cell.Row - 1, cell.Column))
            {
                addCellToArray(cell.Row - 1, cell.Column);
                AddCellToTree(cell, cell.Row - 1, cell.Column);
            }
            if (_cells.GetLength(1) > cell.Column + 1 && isCanMove(cell.Row, cell.Column + 1))
            {
                addCellToArray(cell.Row, cell.Column + 1);
                AddCellToTree(cell, cell.Row, cell.Column + 1);
            }
            if (_cells.GetLength(0) > cell.Row + 1 && isCanMove(cell.Row + 1, cell.Column))
            {
                addCellToArray(cell.Row + 1, cell.Column);
                AddCellToTree(cell, cell.Row + 1, cell.Column);
            }
            if (0 <= cell.Column - 1 && isCanMove(cell.Row, cell.Column - 1))
            {
                addCellToArray(cell.Row, cell.Column - 1);
                AddCellToTree(cell, cell.Row, cell.Column - 1);
            }
        }

        private protected void AddCellToTree(Cell currentCell, int row, int column)
        {
            var currentNode = _cameFrom.FindNode(currentCell);
            currentNode.AddChild(_cells[row, column]);
        }


        private protected void FindPath(Cell finish, TreeNode<Cell> treeNode)
        {
            if (_start != treeNode.Item)
            {
                FindPath(finish, treeNode.Parent);
                BoardModel.SetState(treeNode.Item.Row, treeNode.Item.Column, State.Path);
            }
        }

        private protected bool IsCanMove(int row, int column) =>
            _cells[row, column].State != State.Visited
            && _cells[row, column].State != State.Border
            && _cells[row, column].State != State.Start
            && _cells[row, column].State != State.InQueue;
    }
}