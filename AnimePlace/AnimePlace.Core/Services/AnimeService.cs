using AnimePlace.Core.Contracts;
using AnimePlace.Core.Models;
using AnimePlace.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            context.Database.EnsureCreated();
            var animes = await context.Animes.Select(x => new Anime
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Trailer = x.Trailer
            }).ToListAsync();

            return animes;
        }
    }
}
