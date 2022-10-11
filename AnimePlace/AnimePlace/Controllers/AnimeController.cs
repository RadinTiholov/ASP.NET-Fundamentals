using AnimePlace.Core.Contracts;
using AnimePlace.Core.Models;
using AnimePlace.Core.Services;
using AnimePlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace AnimePlace.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeService animeService;
        private readonly IHttpClientFactory httpClientFactory;

        public IEnumerable<RecentAnime>? RecentAnimes { get; set; }
        public AnimeController(IAnimeService _animeService, IHttpClientFactory httpClientFactory)
        {
            this.animeService = _animeService;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var animes = await animeService.GetAll();

            //Getting recent animes from api;
            var httpClient = httpClientFactory.CreateClient("AnimeApi");
            var httpResponseMessage = await httpClient.GetAsync("recent-release");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                RecentAnimes = await JsonSerializer.DeserializeAsync
                    <IEnumerable<RecentAnime>>(contentStream);
            }

            return View(animes);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            var model = new Anime();
            return View(model);
        }

        [HttpPost]
        [Authorize]
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
            var result = await animeService.Delete(id);
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
