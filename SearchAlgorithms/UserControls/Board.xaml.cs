﻿using Core;
using SearchAlgorithms.Enums;
using SearchAlgorithms.Models;
using SearchAlgorithms.Models.Events;
using System.Collections;
using System.ComponentModel;
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
        #region Routed Events

        public event RoutedEventHandler Start
        {
            add { AddHandler(StartEvent, value); }
            remove { RemoveHandler(StartEvent, value); }
        }

        public event RoutedEventHandler CellSelectedChanged
        {
            add { AddHandler(CellSelectedChangedEvent, value); }
            remove { RemoveHandler(CellSelectedChangedEvent, value); }
        }

        public event RoutedEventHandler StateSelectedChanged
        {
            add { AddHandler(StateSelectedChangedEvent, value); }
            remove { RemoveHandler(StateSelectedChangedEvent, value); }
        }

        public event RoutedEventHandler AlgorithmSelectedChanged
        {
            add { AddHandler(AlgorithmSelectedChangedEvent, value); }
            remove { RemoveHandler(AlgorithmSelectedChangedEvent, value); }
        }


        public static readonly RoutedEvent StartEvent =
            EventManager.RegisterRoutedEvent("Start",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(Board));
        
        public static readonly RoutedEvent CellSelectedChangedEvent =
            EventManager.RegisterRoutedEvent("CellSelectedChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<RoutedSelectedCellEventArgs>),
                typeof(Board));

        public static readonly RoutedEvent StateSelectedChangedEvent =
            EventManager.RegisterRoutedEvent("StateSelectedChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<RoutedSelectedCellEventArgs>),
                typeof(Board));

        public static readonly RoutedEvent AlgorithmSelectedChangedEvent =
            EventManager.RegisterRoutedEvent("AlgorithmSelectedChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<RoutedSelectedAlgorithmEventArgs>),
                typeof(Board));

        #endregion

        public Board()
        {
            InitializeComponent();
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            foreach (var item in e.AddedCells)
            {
                var currentRowIndex = ((DataGrid)sender).Items.IndexOf(item.Item);

                RaiseEvent(new RoutedSelectedCellEventArgs(currentRowIndex, item.Column.DisplayIndex, CellSelectedChangedEvent));
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                if(item is State state)
                    RaiseEvent(new RoutedSelectedStateEventArgs(state, StateSelectedChangedEvent));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(StartEvent));
        }

        private void comboBoxAlgorithms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                if (item is Algorithms algorithm)
                    RaiseEvent(new RoutedSelectedAlgorithmEventArgs(algorithm, AlgorithmSelectedChangedEvent));
            }
        }
    }
}
