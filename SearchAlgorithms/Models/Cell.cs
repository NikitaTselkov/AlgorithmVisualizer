using Prism.Mvvm;
using SearchAlgorithms.Models.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models
{
    public class Cell : BindableBase
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        private State _state;
        public State State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
