using AlgorithmVisualizer.Views;
using Core;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using SearchAlgorithms;
using System;
using System.Reflection;
using System.Windows;

namespace AlgorithmVisualizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<SearchAlgorithmsModule>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
        }
    }
}
