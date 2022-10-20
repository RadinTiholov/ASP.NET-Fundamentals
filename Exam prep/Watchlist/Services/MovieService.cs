using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMoviesService
    {
        private readonly WatchlistDbContext dbContext;
        private readonly UserManager<User> userManager;
        public MovieService(WatchlistDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task AddToWatched(int movieId, string userId)
        {
            var user = await dbContext
               .Users
               .Where(x => x.Id == userId)
               .Include(x => x.UsersMovies)
               .FirstOrDefaultAsync();

            if (user == null) 
            {
                throw new ArgumentException("User not found");
            }

            var movie = await dbContext.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
            {
                throw new ArgumentException("Movie not found");
            }

            if (!user.UsersMovies.Any(x => x.MovieId == movieId))
            {
                var userMovie = new UserMovie()
                {
                    MovieId = movieId,
                    UserId = userId,
                    Movie = movie,
                    User = user
                };

                user.UsersMovies.Add(userMovie);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Create(AddMovieViewModel model)
        {
            var movie = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating
            };
            await dbContext.Movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            var movies = await dbContext.Movies.Include(x => x.Genre).ToListAsync();

            var result = movies.Select(m => new MovieViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                ImageUrl = m.ImageUrl,
                Rating = m.Rating,
                Genre = m?.Genre.Name,
            });

            return result;
        }

        public async Task<IEnumerable<GenreViewModel>> GetGenresAsync()
        {
            List<GenreViewModel> genres = await dbContext
                .Genres
                .Select(x => new GenreViewModel 
                {
                    Name = x.Name,
                    Id = x.Id
                })
                .ToListAsync();

            return genres;
        }

        public async Task RemoveFromWatched(int movieId, string userId)
        {
            var user = await dbContext
               .Users
               .Where(x => x.Id == userId)
               .Include(x => x.UsersMovies)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var movie = user.UsersMovies.FirstOrDefault(x => x.MovieId == movieId);

            if (movie == null) 
            {
                throw new ArgumentException("Movie not found");
            }

            user.UsersMovies.Remove(movie);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> Watched(string userId)
        {
            var user = await dbContext
                .Users
                .Where(x => x.Id == userId)
                .Include(x => x.UsersMovies)
                .ThenInclude(x => x.Movie)
                .ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync();

            return user
                .UsersMovies
                .Select(x => new MovieViewModel() 
                {
                    Director = x.Movie.Director,
                    Title = x.Movie.Title,
                    Genre = x.Movie.Genre.Name,
                    Id = x.MovieId,
                    ImageUrl = x.Movie.ImageUrl,
                    Rating = x.Movie.Rating
                })
                .ToList();
        }
    }
}
