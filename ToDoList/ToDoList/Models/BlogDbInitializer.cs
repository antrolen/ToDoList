using Azure;

namespace ToDoList.Models
{
    public static class BlogDbInitializer
    {
        public static void seed(IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var context = services.ServiceProvider.GetRequiredService<ToDoListDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category() { Name = "Work" });
                context.Categories.Add(new Category() { Name = "Home" });
                context.Categories.Add(new Category() { Name = "Studies" });
                context.Categories.Add(new Category() { Name = "Hobby" });
                context.SaveChanges();
            }

            if (!context.Сompletions.Any())
            {
                context.Сompletions.Add(new Сompletion() { Name = "Done" });
                context.Сompletions.Add(new Сompletion() { Name = "Not Done" });
                context.Сompletions.Add(new Сompletion() { Name = "To Do" });
                context.SaveChanges();
            }
        }

    }
}
