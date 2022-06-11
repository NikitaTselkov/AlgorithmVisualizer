using Core;
using SearchAlgorithms.Models;
using SearchAlgorithms.Models.Events;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SearchAlgorithms.UserControls
{
    /// <summary>
    /// Interaction logic for Board
    /// </summary>
    public partial class Board : UserControl
    {
        public Board()
        {
            InitializeComponent();

            _dataGrid = dataGrid;
        }

        private readonly DataGrid _dataGrid;

        public IEnumerable Items2DArraySource
        {
            get { return (IEnumerable)GetValue(Items2DArraySourceProperty); }
            set { SetValue(Items2DArraySourceProperty, value); }
        }

        public event RoutedEventHandler CellSelectedChanged
        {
            add { AddHandler(CellSelectedChangedEvent, value); }
            remove { RemoveHandler(CellSelectedChangedEvent, value); }
        }



        public static readonly DependencyProperty Items2DArraySourceProperty =
            DependencyProperty.Register("Items2DArraySource",
                typeof(IEnumerable),
                typeof(Board),
                new PropertyMetadata(null, Items2DArraySourceChanged));

        public static readonly RoutedEvent CellSelectedChangedEvent =
            EventManager.RegisterRoutedEvent("CellSelectedChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<RoutedSelectedCellEventArgs>),
                typeof(Board));



        private static void Items2DArraySourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var board = (Board)d;
            var cells = (Cell[,])e.NewValue;

            if (e.OldValue != e.NewValue)
            {
                board.dataGrid.ItemsSource = DataGridHelper.ConvertToDataTable(cells).DefaultView;

                foreach (Cell cell in cells)
                {
                    cell.PropertyChanged += Cell_PropertyChanged;
                }

                board.PaintCells(cells);
            }
            else if (e.NewValue == null)
            {
                foreach (Cell cell in cells)
                {
                    cell.PropertyChanged -= Cell_PropertyChanged;
                }
            }

            void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                board.PaintCell((Cell)sender);
            }
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            foreach (var item in e.AddedCells)
            {
                var currentRowIndex = ((DataGrid)sender).Items.IndexOf(item.Item);

                RaiseEvent(new RoutedSelectedCellEventArgs(currentRowIndex, item.Column.DisplayIndex, CellSelectedChangedEvent));
            }
        }

        private void PaintCells(Cell[,] cells)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    PaintCell(cells[i, j]);
                }
            }
        }

        private void PaintCell(Cell cell)
        {
            DataGridCell dataGridCell = DataGridHelper.GetCell(_dataGrid, cell.Row, cell.Column);
            if (cell != null && dataGridCell != null)
            {
                switch (cell.State)
                {
                    case State.Null:
                        dataGridCell.Background = Brushes.White;
                        break;
                    case State.Border:
                        dataGridCell.Background = Brushes.Black;
                        break;
                }
            }
        }
    }
}
