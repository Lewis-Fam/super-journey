
namespace allTdL.Core.Mvvm
{
    public class SelectableItem<T> : BindableObject
    {
        public SelectableItem(T item)
        {
            SelectedItem = item;
        }
        
        private T _item;
        public T SelectedItem
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

    }
}