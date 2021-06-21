using System.Collections.Generic;

namespace allTdL.Core.Data.Models
{
    public class ToDoItem 
    {    
        public virtual string Description { get; set; }
        public virtual bool IsCompleted { get; set; }
    }

    internal class ToDoItemCollection
    {
        public virtual ICollection<ToDoItem> Items { get; set; } = new List<ToDoItem>();

        private ToDoItem toDoItem;

        private List<ToDoItem> GetToDoItems()
        {
            var rtn = new List<ToDoItem>();
            toDoItem = new ToDoItem
            {
                Description = "A sample task."
            };

            rtn.Add(toDoItem);

            return rtn;
        }
    }
}