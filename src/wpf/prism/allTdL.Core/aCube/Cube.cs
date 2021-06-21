using allTdL.Core.aCube.Internal;

namespace allTdL.Core.aCube
{
    /// <summary>
    /// A cube class.
    /// </summary>
    public class Cube : BaseCube
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cube"/> class.
        /// </summary>
        /// <param name="cubeType">The cube type.</param>
        protected Cube(CubeType cubeType = CubeType.Empty) : base(cubeType)
        {
        }

        ///<inheritdoc/>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? base.ToString() : $"[{CubeType}|{Name}]";
        }
    }
}