using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.RepositoryContracts;
using MovieApp.Core.DTO;
using MovieApp.Core.Helper;
using MovieApp.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Services
{
    public class MovieFavouriteServices : IMovieFavouriteServices
    {
        private readonly IMovieFavouriteRepository _movieFavouriteRepository;

        public MovieFavouriteServices(IMovieFavouriteRepository movieFavouriteRepository)
        {
            _movieFavouriteRepository = movieFavouriteRepository;
        }

        public async Task<MovieFavourite> AddMovieToFavourite(MovieFavourite? movieFavourite)
        {
            if (movieFavourite == null)
                throw new ArgumentNullException(nameof(movieFavourite));

            ValidationModel.ValidateModel(movieFavourite);

            var moviefav = await _movieFavouriteRepository
                .AddMovieToFavourite(movieFavourite);
               
            return moviefav;
        }

        public async Task<bool> DeleteMovieFromFavourite(Guid? movieID, Guid? favouriteID)
        {
            if (!favouriteID.HasValue || !movieID.HasValue)
                throw new ArgumentNullException();

            var movieFavourite = await _movieFavouriteRepository
                .GetMovieFavouriteBy(movieID.Value, favouriteID.Value);

            if(movieFavourite == null)
                return false;

            return await _movieFavouriteRepository
                .DeleteMovieFromFavourite(movieID.Value, favouriteID.Value);
        }

        public async Task<IEnumerable<MovieResponse>> GetFavouriteMoviesByUser(Guid? userID)
        {
            if (userID == null)
                throw new ArgumentNullException();

            var movies = await _movieFavouriteRepository
                .GetFavouriteMoviesByUser(userID.Value);

            return movies.Select(x => x.ToMovieResponse());
        }

        public async Task<int> GetFavouriteMoviesCountByUser(Guid? userID)
        {
            if (userID == null)
                throw new ArgumentNullException();

            return await _movieFavouriteRepository.GetFavouriteMoviesCountByUser(userID.Value);
        }

        public async Task<bool> IsMovieInUserFavourites(Guid? movieID, Guid? userID)
        {
            if(movieID == null || userID==null)
                throw new ArgumentNullException();

            return await _movieFavouriteRepository.
                IsMovieInUserFavourites(movieID.Value, userID.Value);
        }
    }
}
