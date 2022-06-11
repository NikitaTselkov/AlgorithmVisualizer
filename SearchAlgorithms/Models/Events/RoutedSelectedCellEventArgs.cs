using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SearchAlgorithms.Models.Events
{
    public class RoutedSelectedCellEventArgs : RoutedEventArgs
    {
        public int Row { get; private set; }
        public int Column { get; private set; }


        public RoutedSelectedCellEventArgs() { }

        public RoutedSelectedCellEventArgs(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public RoutedSelectedCellEventArgs(int row, int column, RoutedEvent routedEvent) : base(routedEvent)
        {
            Row = row;
            Column = column;
        }
    }
}
