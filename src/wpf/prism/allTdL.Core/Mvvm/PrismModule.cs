using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace allTdL.Core.Mvvm
{
    /// <summary>
    /// Implementation of <see cref="IModule"/>.
    /// </summary>
    public abstract class PrismModule : IModule
    {
        /// <summary>
        ///  Gets the <see cref="IRegionManager"/>.
        /// </summary>
        protected IRegionManager RegionManager { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrismModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        protected PrismModule(IRegionManager regionManager) => RegionManager = regionManager;

        /// <summary>
        /// Registers the types.
        /// </summary>
        /// <param name="containerRegistry">The container registry.</param>
        public abstract void RegisterTypes(IContainerRegistry containerRegistry);
        
        /// <summary>
        /// On initialized.
        /// </summary>
        /// <param name="containerProvider">The container provider.</param>
        public abstract void OnInitialized(IContainerProvider containerProvider);
    }
}