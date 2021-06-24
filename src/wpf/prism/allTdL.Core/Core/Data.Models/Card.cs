using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace allTdL.Core.Data.Models
{
    public interface ICard : IFtb, IStudy
    {
        string Name { get; }
    }

    public class Card : Ftb, ICard
    {
        public Card() 
        {
        }

        /// <inheritdoc />
        public Card(string name = "", string frontText = "", string backText = "") : base()
        {
            if (!string.IsNullOrEmpty(name))
            {
                Name = name;
            }
            else
            {
                Name = DateTime.Now.ToFileTimeUtc().ToString();
            }

            if (!string.IsNullOrEmpty(frontText))
            {
                FrontText = frontText;
            }
            if (!string.IsNullOrEmpty(backText))
            {
                BackText = backText;
            }

            InitStudy();
        }

        public virtual string Answer { get; set; }

        public virtual string Name { get; set; }

        public virtual string Question { get; set; }

        public virtual int RightCount { get; set; }

        public virtual int WrongCount { get; set; }

        /// <summary>
        /// IncreaseCount
        /// </summary>
        /// <param name="rightWrong"></param>
        /// <param name="count"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void IncreaseCount(RightWrong rightWrong, int count = 1)
        {
            switch (rightWrong)
            {
                case RightWrong.Right:
                    RightCount = count + RightCount;
                    break;
                case RightWrong.Wrong:
                    WrongCount = count + WrongCount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rightWrong), rightWrong, null);
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? base.ToString() : $"{Name}";
        }
        private void InitStudy()
        {
            //Question = string.IsNullOrEmpty(FrontText) ? Question : FrontText;
            //Answer = string.IsNullOrEmpty(BackText) ? Answer : BackText;
            Question = HasFront ? FrontText : Question;
            Answer = HasBack ? BackText : Answer;
        }
    }

    public class PersistData<T>
    {
        //void Save<T>(T data, string path)
        //{
        //    File.WriteAllText(path, data);
        //}
    }

    
}