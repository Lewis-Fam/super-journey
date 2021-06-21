namespace allTdL.Core.Data.Models
{
    public interface IFtb
    {
        string FrontText { get; }
        string BackText { get; }
        bool IsInactive { get; }
    }
}