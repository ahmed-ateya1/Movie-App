using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Domain.RepositoryContracts
{
    public interface IMovieRepository
    {
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> GetMovieByID(Guid movieID);
        Task<Movie> GetMovieByTitle(string title);
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<IEnumerable<Movie>> GetFilteredMovie(Expression<Func<Movie,bool>>predict);
        Task<IEnumerable<Movie>> GetMovieByGenreID(Guid genreID);
        Task<Movie> UpdateMovie(Movie movie);
        Task<bool> DeleteMovieByID(Guid movieID);
    }
}
