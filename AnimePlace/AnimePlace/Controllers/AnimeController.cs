using AnimePlace.Core.Contracts;
using AnimePlace.Core.Models;
using AnimePlace.Core.Services;
using Microsoft.AspNetCore.Connections.Features;
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
        public async Task<IActionResult> Add(Anime anime) 
        {
            await animeService.Add(anime);
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await animeService.GetOne(id);
            if (model != null)
            {
                return View(model);
            }
            else 
            {
                return RedirectToAction("Index", "NotFound", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Anime anime) 
        {
            var result = await animeService.Edit(anime);
            if (result != null)
            {
                return RedirectToAction("All");
            }
            else
            {
                return RedirectToAction("Index", "NotFound", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id) 
        {
            var result= await animeService.Delete(id);
            if (result != null)
            {
                return RedirectToAction("All");
            }
            else
            {
                return RedirectToAction("Index", "NotFound", new { area = "" });
            }
        }
    }
}
