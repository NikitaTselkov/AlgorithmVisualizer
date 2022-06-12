using Core;
using Microsoft.Xaml.Behaviors;
using SearchAlgorithms.Models;
using SearchAlgorithms.UserControls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SearchAlgorithms.Behaviors
{
    public class BoardPaintBehavior : Behavior<Board>
    {
        private static DataGrid _dataGrid;
        private static Cell[,] _cells;

        #region Dependency Properties

        public Cell[,] CellsSource
        {
            get { return (Cell[,])GetValue(CellsSourceProperty); }
            set { SetValue(CellsSourceProperty, value); }
        }

        public static readonly DependencyProperty CellsSourceProperty =
            DependencyProperty.Register("CellsSource",
                typeof(Cell[,]),
                typeof(BoardPaintBehavior),
                new PropertyMetadata(null, CellsSourceChanged));

        #endregion

        private static void CellsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            _cells = (Cell[,])e.NewValue;

            if (e.OldValue != e.NewValue)
            {
                _dataGrid.ItemsSource = DataGridHelper.ConvertToDataTable(_cells).DefaultView;

                foreach (Cell cell in _cells)
                {
                    cell.PropertyChanged += Cell_PropertyChanged;
                }

                BoardModel.PaintCells(_cells, _dataGrid);
            }
        }

        private static void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            BoardModel.PaintCell((Cell)sender, _dataGrid);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            _dataGrid = AssociatedObject.dataGrid;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            foreach (Cell cell in _cells)
            {
                cell.PropertyChanged -= Cell_PropertyChanged;
            }
        }
    }
}
