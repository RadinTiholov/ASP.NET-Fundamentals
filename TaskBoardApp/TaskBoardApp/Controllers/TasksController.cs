using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Build.Framework;
using System.Data.Common;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Tasks;
using Task = TaskBoardApp.Data.Entities.Task;

namespace TaskBoardApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskBoardAppDbContext dbContext;

        public TasksController(TaskBoardAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new TaskFormModel()
            {
                Boards = dbContext.Boards.Select(b => new TaskBoardModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(TaskFormModel taskModel)
        {
            if(GetBoards().Any(b => b.Id == taskModel.BoardId)) 
            {
                this.ModelState.AddModelError(nameof(taskModel.BoardId), "Board doesn't exists.");
            }

            string currentUser = GetUserId();
            var task = new Task()
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                CreatedOn = DateTime.Now,
                BoardId = taskModel.BoardId,
                OwnerId = currentUser
            };
            this.dbContext.Task.Add(task);
            this.dbContext.SaveChanges();

            return RedirectToAction("All", "Boards");
        }
        public IActionResult Details(int id) 
        {
            var task = dbContext
                .Task
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel() 
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString(),
                    Owner = t.Owner.UserName,
                    Board = t.Board.Name
                })
                .FirstOrDefault();

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var task = dbContext.Task.Find(id);

            if (task == null) 
            {
                return BadRequest();
            }

            string currentUser = GetUserId();

            if(currentUser != task.OwnerId) 
            {
                return Unauthorized();
            }

            var taskModel = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = GetBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, TaskFormModel taskModel) 
        {
            var task = dbContext.Task.Find(id);
            if (task == null) 
            {
                return NotFound();
            }

            string currentUser = GetUserId();
            if (currentUser != task.OwnerId) 
            {
                return Unauthorized();
            }

            if (GetBoards().Any(b => b.Id == taskModel.BoardId))
            {
                this.ModelState.AddModelError(nameof(taskModel.BoardId), "Board doesn't exists.");
            }

            task.Title = taskModel.Title;
            task.Description = taskModel.Description;
            task.BoardId = taskModel.BoardId;

            dbContext.SaveChanges();

            return RedirectToAction("All", "Boards");

        }
        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var task = dbContext.Task.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            string currentUser = GetUserId();
            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel model = new TaskViewModel()
            {
                Title = task.Title,
                Description = task.Description,
                Id = task.Id
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(TaskViewModel model) 
        {
            var task = dbContext.Task.Find(model.Id);
            if (task == null)
            {
                return NotFound();
            }

            string currentUser = GetUserId();
            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

            dbContext.Task.Remove(task);
            dbContext.SaveChanges();

            return RedirectToAction("All", "Boards");

        }

        private IEnumerable<TaskBoardModel> GetBoards() 
        {
            return dbContext
                .Boards
                .Select(b => new TaskBoardModel() 
                {
                    Id =b.Id,
                    Name =b.Name
                });
        }

        private string GetUserId() 
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
