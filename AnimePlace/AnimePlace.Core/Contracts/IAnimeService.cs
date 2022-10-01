using AnimePlace.Core.Models;

namespace AnimePlace.Core.Contracts
{
    public interface IAnimeService
    {
        Task<IEnumerable<Anime>> GetAll();
    }
}
