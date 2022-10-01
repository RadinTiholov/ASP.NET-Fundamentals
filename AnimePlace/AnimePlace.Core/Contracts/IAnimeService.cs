using AnimePlace.Core.Models;
using System.Threading.Tasks;

namespace AnimePlace.Core.Contracts
{
    public interface IAnimeService
    {
        Task<IEnumerable<Anime>> GetAll();
        Task Add(Anime anime);
        Task<Anime> GetOne(string id);
        Task<Anime> Edit(Anime anime);
        Task<Anime> Delete(string id);
    }
}
