using ForumAppExercise.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumAppExercise.Controllers
{
    public class PostsController : Controller
    {
        private readonly ForumAppDbContext dbContext;
        public PostsController(ForumAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
