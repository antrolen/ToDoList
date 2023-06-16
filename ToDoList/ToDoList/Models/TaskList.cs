using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Where title?")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(20000)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Task main image: ")]
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<TaskСompletion> TaskСompletions { get; set; }

    }
}
