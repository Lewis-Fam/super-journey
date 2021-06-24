namespace allTdL.Core.aCube.Interfaces
{
    public interface ICube
    {
        CubeType CubeType { get; }
        string Name { get; }
        string Tag { get; }
    }
}