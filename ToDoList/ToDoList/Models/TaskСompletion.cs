using Azure;
using Microsoft.Extensions.Hosting;

namespace ToDoList.Models
{
    public class TaskСompletion
    {
        public int Id { get; set; }
        public int TaskListId { get; set; }
        public TaskList TaskList { get; set; }
        public int СompletionId { get; set; }
        public Сompletion Сompletion { get; set; }

    }
}
