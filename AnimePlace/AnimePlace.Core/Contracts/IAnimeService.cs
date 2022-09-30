using AnimePlace.Core.Models;

namespace AnimePlace.Core.Contracts
{
    public interface IAnimeService
    {
        IEnumerable<Anime> GetAll();
    }
}
