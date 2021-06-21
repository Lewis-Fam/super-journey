namespace allTdL.Core.Data.Models
{
    public abstract class Ftb : IFtb
    {
        public string FrontText { get; set; }
        public string BackText { get; set; }
        public bool IsInactive { get; set; }
        protected bool HasFront => !string.IsNullOrEmpty(FrontText);
        protected bool HasBack => !string.IsNullOrEmpty(BackText);
    }
}