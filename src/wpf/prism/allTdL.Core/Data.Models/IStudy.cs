namespace allTdL.Core.Data.Models
{
    public interface IStudy
    {
        public string Question { get;  }
        public string Answer { get;  }
        public int RightCount { get;  }
        public int WrongCount { get;  }
        void IncreaseCount(RightWrong rightWrong, int count = 1);
    }
}