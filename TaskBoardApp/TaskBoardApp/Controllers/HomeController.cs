using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext dbContext;

        public HomeController(TaskBoardAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var taskBoards = dbContext
                .Boards
                .Select(b => b.Name)
                .Distinct();

            var taskCounts = new List<HomeBoardModel>();
            foreach (var boardName in taskBoards) 
            {
                var tasksInBoard = dbContext.Task.Where(t => t.Board.Name == boardName).Count();
                taskCounts.Add(new HomeBoardModel()
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoard
                });
            }

            var userTaskCount = -1;

            if (User.Identity.IsAuthenticated) 
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userTaskCount = dbContext.Task.Where(x => x.OwnerId == currentUserId).Count();
            }

            var homeModel = new HomeViewModel() 
            {
                AllTasksCount = dbContext.Task.Count(),
                BoardsWithTasksCount = taskCounts,
                UserTasksCount = userTaskCount
            };

            return View(homeModel);
        }
    }
}