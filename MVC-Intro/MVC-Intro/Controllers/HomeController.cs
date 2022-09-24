using Microsoft.AspNetCore.Mvc;
using MVC_Intro.Models;
using System.Diagnostics;

namespace MVC_Intro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Some title here";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About() 
        {
            return View();
        }
        public IActionResult Numbers() 
        {
            return View();
        }
        public IActionResult NumbersGen(int count) 
        {
            ViewBag.Count = count;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}