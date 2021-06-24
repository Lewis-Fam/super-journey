using System.Configuration;
using allTdL.Core;
using allTdL.Core.Mvvm;
using allTdL.Mvvm;
using allTdL.Mvvm.Strings;
using Module.Manager.ViewModels;
using Module.Manager.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Module.Manager
{
    public class ManagerModule : PrismModule
    {
        public override void OnInitialized(IContainerProvider containerProvider)
        {

            //https://stackoverflow.com/questions/3131297/prism-and-wpf-how-to-add-a-module-on-demand
            //IModuleManager moduleManager = containerProvider.Resolve<IModuleManager>();
            //ModulesConfigurationSection modules = ConfigurationManager.GetSection("modules") as ModulesConfigurationSection;
            //foreach (ModuleConfigurationElement module in modules.Modules)
            //    moduleManager.LoadModule(module.ModuleName);

            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(() => { LoadModulesOnGuiThread(); }));
            
            RegionManager.RequestNavigate(RegionNames.ShellContentRegion, "MainPage");
        }



        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }

        public ManagerModule(IRegionManager regionManager) : base(regionManager)
        {
        }
    }
}