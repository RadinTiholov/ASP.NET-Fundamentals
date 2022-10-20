using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Watchlist.Contracts;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var movies = await moviesService.GetAllAsync();

            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var genres = await moviesService.GetGenresAsync();
            var model = new AddMovieViewModel()
            {
                Genres = genres
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await moviesService.Create(model);
                return RedirectToAction("All", "Movies");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int movieId) 
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await moviesService.AddToWatched(movieId, userId);

                return RedirectToAction("All", "Movies");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int movieId) 
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await moviesService.RemoveFromWatched(movieId, userId);

                return RedirectToAction("Watched", "Movies");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Watched() 
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var watched = await moviesService.Watched(userId);
            return View(watched);
        }
    }
}
