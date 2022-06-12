using SearchAlgorithms.Enums;
using System.Windows;

namespace SearchAlgorithms.Models.Events
{
    public class RoutedSelectedStateEventArgs : RoutedEventArgs
    {
        public State State { get; init; }

        public RoutedSelectedStateEventArgs(State state, RoutedEvent routedEvent) : base(routedEvent)
        {
            State = state;
        }
    }
}
