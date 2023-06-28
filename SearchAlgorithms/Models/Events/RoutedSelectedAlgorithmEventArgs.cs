using System.Windows;

namespace SearchAlgorithms.Models.Events
{
    public class RoutedSelectedAlgorithmEventArgs : RoutedEventArgs
    {
        public Enums.Algorithms Algorithm { get; init; }

        public RoutedSelectedAlgorithmEventArgs(Enums.Algorithms algorithm, RoutedEvent routedEvent) : base(routedEvent)
        {
            Algorithm = algorithm;
        }
    }
}
