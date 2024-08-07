using MovieApp.Core.Domain.Entites;
using MovieApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Core.ServicesContracts
{
    public interface IMovieFavouriteServices
    {
        Task<MovieFavourite> AddMovieToFavourite(MovieFavourite? movieFavourite);
        Task<bool> DeleteMovieFromFavourite(Guid? movieID, Guid? favouriteID);
        Task<IEnumerable<MovieResponse>> GetFavouriteMoviesByUser(Guid? userID);
        Task<int> GetFavouriteMoviesCountByUser(Guid? userID);
        Task<bool> IsMovieInUserFavourites(Guid? movieID, Guid? userID);
    }
}
