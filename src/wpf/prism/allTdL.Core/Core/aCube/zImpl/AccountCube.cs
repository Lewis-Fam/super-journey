using allTdL.Core.aCube.Interfaces;

namespace allTdL.Core.aCube.zImpl
{
    public class AccountCube : Cube, IAccountCube
    {
        public AccountCube() : base(CubeType.Account)
        {
        }

        public virtual bool IsInactive { get; set; }
        public virtual double CubeNumber { get; set; } = -1;
    }
}