using Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SearchAlgorithms.Models;
using SearchAlgorithms.Models.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SearchAlgorithms.ViewModels
{
    public class SearchAlgorithmsViewModel : BindableBase
    {
        private Cell[,] _cells;
        public Cell[,] Cells
        {
            get { return _cells; }
            set { SetProperty(ref _cells, value); }
        }

        private DelegateCommand<RoutedSelectedCellEventArgs> selectCellCommand;
        public DelegateCommand<RoutedSelectedCellEventArgs> SelectCellCommand =>
            selectCellCommand ?? (selectCellCommand = new DelegateCommand<RoutedSelectedCellEventArgs>(ExecuteSelectCellCommand));

        public SearchAlgorithmsViewModel(IRegionManager regionManager)
        {
            IRegion region = regionManager.Regions[RegionNames.ContentRegion];

            if (region.ActiveViews.FirstOrDefault() is FrameworkElement activeView)
            {
                CreateCells(activeView);
            }
        }

        private void CreateCells(FrameworkElement view)
        {
            Cells = BoardModel.GenerateCellsAsync((int)view.ActualHeight / 20, (int)view.ActualWidth / 20).Result;
        }

        private void ExecuteSelectCellCommand(RoutedSelectedCellEventArgs e)
        {
            Cells[e.Row, e.Column].State = State.Border;
        }
    }
}
