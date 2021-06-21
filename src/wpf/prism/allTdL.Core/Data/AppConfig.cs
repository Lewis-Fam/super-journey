using System;
using System.Collections.Generic;
using System.IO;

namespace allTdL.Core.Data
{
    public class AppConfig : IMetaData
    {
        public AppConfig(string taskFileNamePath = "task.json", string todoFileNamePath = "todo.json")
        {
            TaskFileNamePath = Path.Combine(taskFileNamePath);
            TodoFileNamePath = Path.Combine(todoFileNamePath);
            UserName = Environment.UserName;
        }

        public Dictionary<string, object> MetaData { get; set; }
        public string TaskFileNamePath { get; set; } 
        public string TodoFileNamePath { get; set; } 
        public string UserName { get; set; }
    }

    public interface IMetaData
    {
        Dictionary<string, object> MetaData { get; set; }
    }
   
    public class Payload<T>
    {
        public virtual T Result { get; set; }
        public virtual T Next { get; set; }
    }

    public enum FtbType
    {
        Front,
        Back,
    }
}
