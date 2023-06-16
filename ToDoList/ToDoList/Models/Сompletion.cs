namespace ToDoList.Models
{
    public class Сompletion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TaskСompletion> TaskСompletions { get; set; }

    }
}
