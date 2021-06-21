using allTdL.Core.aCube.Interfaces;

namespace allTdL.Core.aCube.Internal
{
    public abstract class BaseCube : ICube
    {
        public CubeType CubeType { get; }
        public string Name { get; set; }
        public string Tag { get; set; }
        protected BaseCube(CubeType cubeType)
        {
            CubeType = cubeType;
        }

       
    }
}