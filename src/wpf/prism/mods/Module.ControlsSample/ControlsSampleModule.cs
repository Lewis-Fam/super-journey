using allTdL.Core;
using allTdL.Core.Mvvm;
using allTdL.Core.Strings;
using Module.ControlsSample.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Module.ControlsSample
{
    public class ControlsSampleModule : PrismModule
    {
        public ControlsSampleModule(IRegionManager regionManager) : base(regionManager)
        {
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ShellContentRegion, typeof(ViewA));
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}