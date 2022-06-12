using Core.Attributes;
using System.Windows;

namespace SearchAlgorithms.Enums
{
    public enum State
    {
        Border,
        Null,
        [Visibility(Visibility.Collapsed)]
        Visited,
        [Visibility(Visibility.Collapsed)]
        Path
    }
}
