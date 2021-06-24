using System;
using System.Diagnostics;
using allTdL.Core.Mvvm;
using allTdL.Mvvm.Strings;
using Prism.Commands;
using Prism.Regions;

namespace allTdL.Mvvm
{
    /// <summary>The navigation view model base.</summary>
    public abstract class NavigationViewModelBase : ViewModelBase, IConfirmNavigationRequest
    {
        private DelegateCommand<string> _navigateCommand;

        /// <summary>Initializes a new instance of the <see cref="NavigationViewModelBase"/> class.</summary>
        /// <param name="regionManager">The region manager.</param>
        protected NavigationViewModelBase(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        /// <summary>Gets the navigate command.</summary>
        public DelegateCommand<string> NavigateCommand => _navigateCommand ??= new DelegateCommand<string>(OnNavigate);

        /// <summary>Gets the region manager.</summary>
        protected IRegionManager RegionManager { get; }

        /// <summary>Confirms the navigation request.</summary>
        /// <param name="navigationContext">   The navigation context.</param>
        /// <param name="continuationCallback">The continuation callback.</param>
        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        /// <summary>Is navigation target?</summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>A bool.</returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Debug.WriteLine($"{nameof(IsNavigationTarget)}|{navigationContext?.Uri}");
            return true;
        }

        /// <summary>On navigated from.</summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>On navigated to.</summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <remarks>Initialize here.</remarks>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        /// <summary>On Navigate</summary>
        /// <param name="view">The view.</param>
        protected virtual void OnNavigate(string view)
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, view);
        }
    }
}