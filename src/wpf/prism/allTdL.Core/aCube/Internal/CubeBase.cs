using allTdL.Core.aCube.Interfaces;

namespace allTdL.Core.aCube.Internal
{
    public abstract class CubeBase : ICube
    {
        public CubeType CubeType { get; }
        public string Name { get; set; }
        public string Tag { get; set; }
        protected CubeBase(CubeType cubeType)
        {
            CubeType = cubeType;
        }

        //protected CubeBase()
        //{
        //    CubeType = CubeType.Empty;
        //}
    }
}