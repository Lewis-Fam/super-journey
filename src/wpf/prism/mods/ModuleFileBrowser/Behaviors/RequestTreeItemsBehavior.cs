using System.Collections;
using Microsoft.Xaml.Behaviors;
using ModuleFileBrowser.Models;
using ModuleFileBrowser.ViewModels;
using Syncfusion.UI.Xaml.TreeGrid;

namespace ModuleFileBrowser.Behaviors
{
    public class RequestTreeItemsBehavior : Behavior<SfTreeGrid>
    {
        private FileExplorerViewModel fileExplorerViewModel;
        //private EmployeeInfoViewModel viewModel;

        protected override void OnAttached()
        {
            base.OnAttached();
            //if(this.AssociatedObject.DataContext is EmployeeInfoViewModel)
            //    viewModel = this.AssociatedObject.DataContext as EmployeeInfoViewModel;
            //else
            fileExplorerViewModel = this.AssociatedObject.DataContext as FileExplorerViewModel;

            this.AssociatedObject.RequestTreeItems += AssociatedObject_RequestTreeItems;
        }

        void AssociatedObject_RequestTreeItems(object sender, TreeGridRequestTreeItemsEventArgs args)
        {
             
            if (args.ParentItem == null)
            {
                //if(viewModel != null)
                ////get the root list - get all employees who have no boss 
                //    args.ChildItems = viewModel.EmployeeList.Where(x => x.ReportsTo == -1); //get all employees whose boss's id is -1 (no boss)
                //else
                args.ChildItems = (IEnumerable)fileExplorerViewModel.DriveDetails;

            }
            else 
                //if ParentItem not null, then set args.ChildList to the child items for the given ParentItem.
            {   //get the children of the parent object
                //if (viewModel != null)
                //{
                //    EmployeeInfo emp = args.ParentItem as EmployeeInfo;
                //    if (emp != null)
                //    {
                //        //get all employees that report to the parent employee
                //        args.ChildItems = viewModel.GetReportees(emp.ID);
                //    }
                //}
                //else
                //{
                FileInfoModel item = args.ParentItem as FileInfoModel;
                args.ChildItems = fileExplorerViewModel.GetChildFolderContent(item);
                //}
            }
        }       

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.RequestTreeItems -= AssociatedObject_RequestTreeItems;
        }
    }
}