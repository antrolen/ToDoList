using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using ToDoList.Extensions;
using ToDoList.Helpers;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ToDoListDbContext toDoListDbContext;

        public HomeController(ToDoListDbContext toDoListDbContext)
        {
            this.toDoListDbContext = toDoListDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(toDoListDbContext.TaskLists.Include(x=> x.TaskСompletions).ThenInclude(x=> x.Сompletion).Include(x=> x.Category));
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            TaskList taskList = toDoListDbContext.TaskLists.Include(x=> x.TaskСompletions).ThenInclude(x=> x.Сompletion).Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            return View(taskList);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            TaskList taskList = toDoListDbContext.TaskLists.FirstOrDefault(x => x.Id == id);

            ViewBag.Categories = new SelectList(toDoListDbContext.Categories, "Id", "Name", toDoListDbContext.Categories.Find(id));
            var selectedComplIds = toDoListDbContext.TaskСompletions.Where(x => x.TaskListId == id).Select(x => x.СompletionId);
            ViewBag.Сompletions = new MultiSelectList(toDoListDbContext.Сompletions, "Id", "Name", selectedComplIds);
            return View(taskList);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskList taskList, IFormFile ImageUrl, int[] compls)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    taskList.ImageUrl = await FileUploadHelper.UploadAsync(ImageUrl);
                }
                catch (Exception)
                {
                   
                }

                taskList.Date = DateTime.Now;
                toDoListDbContext.TaskLists.Update(taskList);
                await toDoListDbContext.SaveChangesAsync();


                var taskWithCompls = toDoListDbContext.TaskLists.Include(x => x.TaskСompletions).FirstOrDefault(x => x.Id == taskList.Id);
                toDoListDbContext.UpdateManyToMany(
                    taskWithCompls.TaskСompletions,
                    compls.Select(x => new TaskСompletion { СompletionId = x, TaskListId = taskList.Id }),
                    x => x.СompletionId);

                await toDoListDbContext.SaveChangesAsync();
              

                TempData["status"] = "   Task edit!";
                return RedirectToAction("Index", "Home");
            }
            return View("AddTask", taskList);
        }


        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = new SelectList(toDoListDbContext.Categories, "Id", "Name");
            ViewBag.Сompletions = new MultiSelectList(toDoListDbContext.Сompletions, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TaskList taskList, IFormFile ImageUrl, int[] compls)
        {

            if (ModelState.IsValid)
            {
                taskList.ImageUrl = await FileUploadHelper.UploadAsync(ImageUrl);

                taskList.Date = DateTime.Now;
                await toDoListDbContext.AddAsync(taskList);
                await toDoListDbContext.SaveChangesAsync();

                toDoListDbContext.TaskСompletions.AddRange(compls.Select(x => new TaskСompletion { СompletionId = x, TaskListId = taskList.Id }));

                await toDoListDbContext.SaveChangesAsync();


                TempData["status"] = "   New task added!";
                return RedirectToAction("Index", "Home");
            }
            return View("AddTask", taskList);
        }


        [HttpPost] 
        public async Task<IActionResult> Delete(int? id) { 
            if (id != null) { 
                TaskList taskList = new TaskList {Id=id.Value }; 
                toDoListDbContext.Entry(taskList).State = EntityState.Deleted; 
                await toDoListDbContext.SaveChangesAsync(); 
                return RedirectToAction("Index"); 
                } 
                return NotFound(); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}