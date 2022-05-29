using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Core;
using SearchAlgorithms.Views;
using System.Windows.Controls;
using Prism.Mvvm;
using SearchAlgorithms.ViewModels;

namespace SearchAlgorithms
{
    public class SearchAlgorithmsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public SearchAlgorithmsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentControl));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SearchAlgorithmsView, SearchAlgorithmsViewModel>();
        }
    }
}