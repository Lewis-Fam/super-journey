using allTdL.Core.Mvvm;
using ModuleA.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleA
{
    public class ModuleAModule : PrismModule
    {
        public override void OnInitialized(IContainerProvider containerProvider)
        {
            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(PersonList));            
            //regionManager.RegisterViewWithRegion("LeftRegion", typeof(MessageView));
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PersonList>();
            containerRegistry.RegisterForNavigation<PersonDetail>();
        }
        public ModuleAModule(IRegionManager regionManager) : base(regionManager)
        {
        }
    }
}