using Microsoft.AspNetCore.Http;
using MovieApp.Core.Domain.Entites;
using MovieApp.Core.DTO;
using MovieApp.Core.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.ServicesContracts
{
    public interface IMovieServices
    {
        Task<MovieResponse> AddMovie(MovieAddRequest? movieAddRequest);
        Task<IEnumerable<MovieResponse>> GetAllMovies();
        Task<IEnumerable<MovieResponse>> GetMovieByGenreID(Guid? genreID);
        Task<IEnumerable<MovieResponse>> GetFilteredMovies(string searchBy , string? searchString);
        Task<MovieResponse?> GetMovieByMovieID(Guid? movieID);
        Task<IEnumerable<MovieResponse>> SortedMovies(List<MovieResponse> movies , string sortBy, SortedOption sortedOption);
        Task<bool> DeleteMovieByMovieID(Guid? movieID);
        Task<MovieResponse> UpdateMovies(MovieUpdateRequest? movieUpdateRequest);
    }
}
