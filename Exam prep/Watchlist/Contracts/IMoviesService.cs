using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Contracts
{
    public interface IMoviesService
    {
        Task<IEnumerable<MovieViewModel>> GetAllAsync();
        Task<IEnumerable<GenreViewModel>> GetGenresAsync();

        Task Create(AddMovieViewModel model);

        Task AddToWatched(int movieId, string userId);
        Task RemoveFromWatched(int movieId, string userId);
        Task<IEnumerable<MovieViewModel>> Watched(string userId);
    }
}
