using Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SearchAlgorithms.Enums;
using SearchAlgorithms.Models;
using SearchAlgorithms.Models.Algorithms;
using SearchAlgorithms.Models.Events;
using System;
using System.Linq;
using System.Windows;

namespace SearchAlgorithms.ViewModels
{
    public class SearchAlgorithmsViewModel : BindableBase
    {
        public static Cell[,] Cells => BoardModel.GetCells();
        private Cell _startCell;
        private State _currentState;

        #region Commands

        private DelegateCommand<RoutedSelectedCellEventArgs> selectCellCommand;
        public DelegateCommand<RoutedSelectedCellEventArgs> SelectCellCommand =>
            selectCellCommand ?? (selectCellCommand = new DelegateCommand<RoutedSelectedCellEventArgs>(ExecuteSelectCellCommand));

        private DelegateCommand<RoutedSelectedStateEventArgs> selectStateCommand;
        public DelegateCommand<RoutedSelectedStateEventArgs> SelectStateCommand =>
            selectStateCommand ?? (selectStateCommand = new DelegateCommand<RoutedSelectedStateEventArgs>(ExecuteSelectStateCommand));

        private DelegateCommand<RoutedEventArgs> startCommand;
        public DelegateCommand<RoutedEventArgs> StartCommand =>
            startCommand ?? (startCommand = new DelegateCommand<RoutedEventArgs>(ExecuteStartCommand));

        #endregion

        public SearchAlgorithmsViewModel(IRegionManager regionManager)
        {
            IRegion region = regionManager.Regions[RegionNames.ContentRegion];

            if (region.ActiveViews.FirstOrDefault() is FrameworkElement activeView)
            {
                CreateCellsAsync(activeView);
            }
        }

        private static async void CreateCellsAsync(FrameworkElement view)
        {
            await BoardModel.GenerateCellsAsync((int)view.ActualHeight / 20, (int)view.ActualWidth / 20);
        }

        private void ExecuteSelectCellCommand(RoutedSelectedCellEventArgs e)
        {
            if (_currentState == State.Start)
                _startCell = Cells[e.Row, e.Column];

            BoardModel.SetState(e.Row, e.Column, _currentState);
        }

        private void ExecuteSelectStateCommand(RoutedSelectedStateEventArgs e)
        {
            _currentState = e.State;
        }

        private void ExecuteStartCommand(RoutedEventArgs e)
        {
            Bfs bfs = new Bfs(Cells, _startCell);
            bfs.StartSearch().ConfigureAwait(true);
        }
    }
}
