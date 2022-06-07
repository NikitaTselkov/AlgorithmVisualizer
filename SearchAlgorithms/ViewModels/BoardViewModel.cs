using Prism.Mvvm;
using SearchAlgorithms.Models;
using System;
using System.Data;

namespace SearchAlgorithms.ViewModels
{
    public class BoardViewModel : BindableBase
    {
        private DataTable _cells = new();
        public DataTable Cells
        {
            get { return _cells; }
            set { SetProperty(ref _cells, value); }
        }

        public BoardViewModel()
        {
            //TODO: Убрать числа.
            Cells = BoardModel.GenerateCellsAsync(85, 50).Result;
        }
    }
}
