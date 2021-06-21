using ModuleFileBrowser.ViewModels;
using ModuleFileBrowser.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleFileBrowser
{
    public class ModuleFileBrowserModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FileExplorer, FileExplorerViewModel>();
        }
    }
}