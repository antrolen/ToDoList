using Microsoft.Extensions.Hosting;

namespace ToDoList.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TaskList> TaskLists { get; set; }

    }
}
