using allTdL.Core.aCube.Internal;

namespace allTdL.Core.aCube
{


    public class Cube : CubeBase
    {
        public Cube(CubeType cubeType = CubeType.Empty) : base(cubeType)
        {
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? base.ToString() : $"[{CubeType}|{Name}]";
        }
    }
}