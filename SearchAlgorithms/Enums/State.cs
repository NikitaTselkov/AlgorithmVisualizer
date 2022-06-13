using Core.Attributes;
using System.Windows;

namespace SearchAlgorithms.Enums
{
    public enum State
    {
        Border,
        Start,
        Finish,
        Null,
        [Visibility(Visibility.Collapsed)]
        Visited,
        [Visibility(Visibility.Collapsed)]
        InQueue,
        [Visibility(Visibility.Collapsed)]
        Path
    }
}
