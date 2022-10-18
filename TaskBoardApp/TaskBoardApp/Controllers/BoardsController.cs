using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TaskBoardApp.Data;
using TaskBoardApp.Models;
using TaskBoardApp.Models.Tasks;

namespace TaskBoardApp.Controllers
{
    public class BoardsController : Controller
    {
        private readonly TaskBoardAppDbContext dbContext;

        public BoardsController(TaskBoardAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult All() 
        {
            var boards = dbContext.Boards
                .Select(b => new BoardViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new TaskViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.Owner.UserName
                    })
                })
                .ToList();

            return View(boards);
        }
    }
}
