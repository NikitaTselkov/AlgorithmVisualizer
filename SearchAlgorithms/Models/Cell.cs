using Prism.Mvvm;
using SearchAlgorithms.Enums;

namespace SearchAlgorithms.Models
{
    public class Cell : BindableBase
    {
        public int Row { get; init; }
        public int Column { get; init; }

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
    }
}
