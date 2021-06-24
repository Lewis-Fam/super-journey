using System.Configuration;

using allTdL.Core.Mvvm;
using allTdL.Mods.ToDo.Views;
using allTdL.Mvvm;
using Prism.Ioc;
using Prism.Regions;

namespace allTdL.Mods.ToDo
{
    /// <summary>
    /// The to do module class. Inherits from <see cref="PrismModule"/>
    /// </summary>
    public class ToDoModule : PrismModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public ToDoModule(IRegionManager regionManager) : base(regionManager)
        {
        }

        ///<inheritdoc/>
        public override void OnInitialized(IContainerProvider containerProvider)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(ConfigurationManager.AppSettings.Get("SyncFusionKey"));
        }

        ///<inheritdoc/>
        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CheckListView>();
            containerRegistry.RegisterForNavigation<ViewFtbList>();
            containerRegistry.RegisterForNavigation<ViewCard>();
            containerRegistry.RegisterForNavigation<ViewToDoItemList>();
            containerRegistry.RegisterForNavigation<ViewToDoItem>();
            containerRegistry.RegisterForNavigation<Notepad>();
        }
    }
}