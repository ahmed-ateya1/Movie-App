using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.RepositoryContracts;
using MovieApp.Infrastructure.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _db;

        public MovieRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            await _db.Movies.AddAsync(movie);
            await _db.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> DeleteMovieByID(Guid movieID)
        {
            var movie = await GetMovieByID(movieID);
            if (movie == null) return false;
            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _db.Movies
                .Include(x=>x.Genre)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetFilteredMovie(Expression<Func<Movie, bool>> predict)
        {
            return await _db.Movies
                .Include(x=>x.Genre) 
                .AsNoTracking()
                 .Where(predict).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovieByGenreID(Guid genreID)
        {
            return await _db.Movies.Include(x => x.Genre)
                .AsNoTracking()
                .Where(x=>x.GenreID == genreID)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieByID(Guid movieID)
        {
            return await _db.Movies.Include(x=>x.Genre)
               .FirstOrDefaultAsync(x => x.MovieID == movieID);
        }

        public async Task<Movie> GetMovieByTitle(string title)
        {
            return await _db.Movies.Include(x => x.Genre)
              .FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            var oldMovie = await GetMovieByID(movie.MovieID);
            if (oldMovie == null) return null;

            oldMovie.Title = movie.Title;
            oldMovie.MovieID = movie.MovieID;
            oldMovie.Rating = movie.Rating;
            oldMovie.ReleaseDate = movie.ReleaseDate;
            oldMovie.ImageURL = movie.ImageURL;
            oldMovie.GenreID = movie.GenreID;

            await _db.SaveChangesAsync();
            return oldMovie;
        }
    }
}
