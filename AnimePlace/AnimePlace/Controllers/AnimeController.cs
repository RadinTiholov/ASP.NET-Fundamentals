using AnimePlace.Core.Contracts;
using AnimePlace.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimePlace.Controllers
{
    public class AnimeController: Controller
    {
        private readonly IAnimeService animeService;

        public AnimeController(IAnimeService _animeService)
        {
            this.animeService = _animeService;
        }

        [HttpGet]
        public async Task<IActionResult> All() 
        {
            var animes = await animeService.GetAll();
            return View(animes);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            var model = new Anime();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Anime anime) 
        {
            ;
            return RedirectToAction("All");
        }
    }
}
