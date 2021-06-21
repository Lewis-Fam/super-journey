namespace allTdL.Core.aCube.Interfaces
{
    public interface IAccountCube : ICube
    {
        double CubeNumber { get; }
        bool IsInactive { get; }
    }
}