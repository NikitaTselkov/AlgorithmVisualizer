using Prism.Mvvm;
using SearchAlgorithms.Enums;
using System;

namespace SearchAlgorithms.Models
{
    public class Cell : BindableBase, IComparable
    {
        public int Row { get; init; }
        public int Column { get; init; }
        public int Weight { get; set; }

        private State _state;
        public State State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        public Cell(int row, int column, State state = State.Null)
        {
            Row = row;
            Column = column;
            State = state;
        }

        public int CompareTo(object obj)
        {
            return Weight.CompareTo((obj as Cell).Weight);
        }
    }
}
