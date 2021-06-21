using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using allTdL.Core.Data.Models;
using allTdL.Mods.ToDo.ViewModels;
using Prism.Common;
using Prism.Regions;

namespace allTdL.Mods.ToDo.Views
{
    /// <summary>
    /// Interaction logic for ViewTaskItem.xaml
    /// </summary>
    public partial class ViewToDoItem : UserControl
    {
        public ViewToDoItem()
        {
            InitializeComponent();
            RegionContext.GetObservableContext(this).PropertyChanged += ViewTaskItem_PropertyChanged;
        }

        private void ViewTaskItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var context = (ObservableObject<object>)sender;
            var selectedPerson = (ToDoItem)context.Value;
            (DataContext as ViewToDoItemViewModel).SelectedItem = selectedPerson;
        }
    }
}

