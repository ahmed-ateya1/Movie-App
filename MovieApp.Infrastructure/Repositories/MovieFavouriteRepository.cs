using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.IdentityEntites;
using MovieApp.Core.Domain.RepositoryContracts;
using MovieApp.Infrastructure.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Repositories
{
    public class MovieFavouriteRepository : IMovieFavouriteRepository
    {
        private readonly AppDbContext _db;
        private readonly IMovieRepository _movieRepository;

        public MovieFavouriteRepository(AppDbContext db, IMovieRepository movieRepository)
        {
            _db = db;
            _movieRepository = movieRepository;
        }

        public async Task<MovieFavourite> AddMovieToFavourite(MovieFavourite movieFavourite)
        {
            await _db.MovieFavourites.AddAsync(movieFavourite);
            await _db.SaveChangesAsync();
            return movieFavourite;
        }

        public async Task<bool> DeleteMovieFromFavourite(Guid movieID, Guid favouriteID)
        {
            var movieFavourite = await GetMovieFavouriteBy(movieID, favouriteID);
            if (movieFavourite == null)
                return false;
            _db.MovieFavourites.Remove(movieFavourite);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Movie>> GetFavouriteMoviesByUser(Guid userID)
        {
            var user = await _db.Users
                .Include(u => u.Favourite)
                .FirstOrDefaultAsync(x => x.Id == userID);

            if (user?.Favourite == null)
                return new List<Movie>();

            var favouriteMovies = await _db.MovieFavourites
                .Where(x => x.FavouriteID == user.Favourite.FavouriteID)
                .ToListAsync();

            var result = new List<Movie>();
            foreach (var movie in favouriteMovies)
            {
                var movieDetails = await _movieRepository.GetMovieByID(movie.MovieID);
                if (movieDetails != null)
                {
                    result.Add(movieDetails);
                }
            }
            return result;
        }

        public async Task<int> GetFavouriteMoviesCountByUser(Guid userID)
        {
            var user = await _db.Users
                .Include(u => u.Favourite)
                .FirstOrDefaultAsync(x => x.Id == userID);

            if (user?.Favourite == null)
                return 0;

            return await _db.MovieFavourites
                .CountAsync(x => x.FavouriteID == user.Favourite.FavouriteID);
        }

        public async Task<MovieFavourite> GetMovieFavouriteBy(Guid movieID, Guid favouriteID)
        {
            return await _db.MovieFavourites
                .FirstOrDefaultAsync(x => x.MovieID == movieID && x.FavouriteID == favouriteID);
        }

        public async Task<bool> IsMovieInUserFavourites(Guid movieID, Guid userID)
        {
            var user = await _db.Users
                .Include(u => u.Favourite)
                .FirstOrDefaultAsync(x => x.Id == userID);

            if (user?.Favourite == null)
                return false;

            return await _db.MovieFavourites
                .AnyAsync(x => x.MovieID == movieID && x.FavouriteID == user.Favourite.FavouriteID);
        }
    }
}
