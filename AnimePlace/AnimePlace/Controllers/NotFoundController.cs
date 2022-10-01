using Microsoft.AspNetCore.Mvc;

namespace AnimePlace.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
