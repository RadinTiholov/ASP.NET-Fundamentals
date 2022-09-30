using AnimePlace.Core.Contracts;
using AnimePlace.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimePlace.Core.Services
{
    public class AnimeService : IAnimeService
    {
        public Task<IEnumerable<Anime>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
