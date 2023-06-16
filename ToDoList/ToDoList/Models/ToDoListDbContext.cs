using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ToDoList.Models
{
    public class ToDoListDbContext: DbContext
    {
        public ToDoListDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskСompletion>()
                 .HasOne(pt => pt.TaskList)
                 .WithMany(p => p.TaskСompletions)
                 .HasForeignKey(pt => pt.TaskListId);

            modelBuilder.Entity<TaskСompletion>()
                 .HasOne(pt => pt.Сompletion)
                 .WithMany(p => p.TaskСompletions)
            .HasForeignKey(pt => pt.СompletionId);
        }

        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Сompletion> Сompletions { get; set; }
        public DbSet<TaskСompletion> TaskСompletions { get; set; }

    }
}
