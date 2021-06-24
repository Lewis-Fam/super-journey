namespace allTdL.Core.Mvvm.Interfaces
{
    ///<summary>
    ///Current selection in the application.
    ///</summary>
    public interface ISelection<TSelectedItem>
    {
        int SelectedIndex {get; set;}
        TSelectedItem SelectedItem { get; set; }
    }
}