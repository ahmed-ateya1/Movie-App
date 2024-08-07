using MovieApp.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Core.Domain.RepositoryContracts
{
    public interface IMovieFavouriteRepository
    {
        Task<MovieFavourite> AddMovieToFavourite(MovieFavourite movieFavourite);
        Task<bool> DeleteMovieFromFavourite(Guid movieID, Guid favouriteID);
        Task<MovieFavourite> GetMovieFavouriteBy(Guid movieID , Guid favouriteID);
        Task<IEnumerable<Movie>> GetFavouriteMoviesByUser(Guid userID);
        Task<bool> IsMovieInUserFavourites(Guid movieID, Guid userID);
        Task<int> GetFavouriteMoviesCountByUser(Guid userID);
    }
}
