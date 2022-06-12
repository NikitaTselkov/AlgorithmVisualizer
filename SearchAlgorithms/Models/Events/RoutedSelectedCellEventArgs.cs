using System.Windows;

namespace SearchAlgorithms.Models.Events
{
    public class RoutedSelectedCellEventArgs : RoutedEventArgs
    {
        public int Row { get; init; }
        public int Column { get; init; }

        public RoutedSelectedCellEventArgs(int row, int column, RoutedEvent routedEvent) : base(routedEvent)
        {
            Row = row;
            Column = column;
        }
    }
}
