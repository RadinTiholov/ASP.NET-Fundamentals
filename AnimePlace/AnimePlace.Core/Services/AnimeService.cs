using AnimePlace.Core.Contracts;
using AnimePlace.Core.Models;
using AnimePlace.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnimePlace.Core.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly AnimePlaceDbContext context;
        public AnimeService(AnimePlaceDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Anime>> GetAll()
        {
            var animes = await context.Animes.Select(x => new Anime
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Trailer = x.Trailer,
                Image = x.Image
            }).ToListAsync();

            return animes;
        }

        public async Task Add(Anime anime) 
        {
            await context.Animes.AddAsync(anime);
            await context.SaveChangesAsync();
        }

        public async Task<Anime> GetOne(string id)
        {
            var anime = await context.Animes.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            return anime;
        }

        public async Task<Anime> Edit(Anime anime)
        {
            var foundAnime = await context.Animes.FirstOrDefaultAsync(x => x.Id.ToString() == anime.Id.ToString());
            if (foundAnime != null) 
            {
                foundAnime.Title = anime.Title;
                foundAnime.Description = anime.Description;
                foundAnime.Image = anime.Image;
                foundAnime.Trailer = anime.Trailer;
                await context.SaveChangesAsync();
            }
            return foundAnime;
        }

        public async Task<Anime> Delete(string id)
        {
            var foundAnime = await context.Animes.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            if (foundAnime != null)
            {
                context.Animes.Remove(foundAnime);
                await context.SaveChangesAsync();
            }
            return foundAnime;
        }
    }
}
