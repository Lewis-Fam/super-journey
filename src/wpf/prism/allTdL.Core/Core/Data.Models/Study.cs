using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace allTdL.Core.Data.Models
{
    /// <summary>
    /// The study.
    /// </summary>
    public class Study<TStudyItem> where TStudyItem : IStudy
    {
        public ICollection<TStudyItem> StudyCards { get; set; }

        public void StartStudying()
        {
            Stopwatch = Stopwatch.StartNew();
        }

        public void StopStudying()
        {
            Stopwatch.Stop();
            TimeSpentStudying += Stopwatch.Elapsed;
        }

        public TimeSpan TimeSpentStudying { get; set; }

        public Stopwatch Stopwatch { get; set; }

    }

    public class TestingSomeShit
    {
        void Test()
        {
            var s = new Study<Card>();
        }
    }
}