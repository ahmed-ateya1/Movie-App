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
using MovieApp.Core.Enumerator;
using Microsoft.AspNetCore.Http;

namespace MovieApp.Core.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IFileServices _fileServices;
        public MovieServices(IMovieRepository movieRepository , IFileServices fileServices)
        {
            _movieRepository = movieRepository;
            _fileServices = fileServices;
        }

        public async Task<MovieResponse> AddMovie(MovieAddRequest? movieAddRequest)
        {
            if(movieAddRequest == null)
                throw new ArgumentNullException(nameof(movieAddRequest));

            if(movieAddRequest.Poster == null)
                throw new ArgumentNullException(nameof(MovieAddRequest.Poster));

            string filename = await _fileServices.CreateFile(movieAddRequest.Poster);

            movieAddRequest.ImageURL = filename;
            
            ValidationModel.ValidateModel(movieAddRequest);

            var movie = movieAddRequest.ToMovie();
            

            movie.MovieID = Guid.NewGuid();

            await _movieRepository.AddMovie(movie);

            return movie.ToMovieResponse();
        }

        public async Task<bool> DeleteMovieByMovieID(Guid? movieID)
        {
            if (movieID == null) throw new ArgumentNullException(nameof(movieID));

            var movie = await GetMovieByMovieID(movieID);

            if (movie == null)
                return false;

            await _fileServices.DeleteFile(movie.ImageURL);
            return await _movieRepository.DeleteMovieByID(movieID.Value);
        }


        public async Task<IEnumerable<MovieResponse>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMovies();
            
            return movies.Select(x=>x.ToMovieResponse()).ToList();
        }

        public async Task<IEnumerable<MovieResponse>> GetFilteredMovies(string searchBy, string? searchString)
        {
           
            if(!string.IsNullOrEmpty(searchString))
                searchString = searchString.ToLower();

            List<Movie> matchedMovies = searchBy switch
            {
                nameof(Movie.Title) =>
                (await _movieRepository.GetFilteredMovie(temp => temp.Title.ToLower()
                    .Contains(searchString))).ToList(),

                nameof(Movie.Description) =>
                (await _movieRepository.GetFilteredMovie(temp => temp.Description.ToLower()
                    .Contains(searchString))).ToList(),

                nameof(Movie.Genre) =>
                (await _movieRepository.GetFilteredMovie(temp => temp.Genre.GenreName.ToLower()
                    .Contains(searchString))).ToList(),

                _ => (await _movieRepository.GetAllMovies()).ToList()
            };

            return matchedMovies.Select(x => x.ToMovieResponse());
        }




        public async Task<IEnumerable<MovieResponse>> GetMovieByGenreID(Guid? genreID)
        {
            if(genreID == null) throw new ArgumentNullException();

            var movies = await _movieRepository.GetMovieByGenreID(genreID.Value);

            return movies.Select(x=>x.ToMovieResponse());
        }

        public async Task<MovieResponse?> GetMovieByMovieID(Guid? movieID)
        {
            if(movieID == null) throw new ArgumentNullException();

            Movie? movie = await _movieRepository.GetMovieByID(movieID.Value);

            if (movie == null)
                return null;

            return movie.ToMovieResponse();
        }

        public async Task<IEnumerable<MovieResponse>> SortedMovies(List<MovieResponse> movies, string sortBy, SortedOption sortedOption)
        {
            if (movies == null) throw new ArgumentNullException(nameof(movies));

            var sortedMovies = (sortBy, sortedOption) switch
            {
                (nameof(Movie.Title), SortedOption.ASC) => movies.OrderBy(x => x.Title, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(Movie.Title), SortedOption.DESC) => movies.OrderByDescending(x => x.Title, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(Movie.Rating), SortedOption.ASC) => movies.OrderBy(x => x.Rating).ToList(),
                (nameof(Movie.Rating), SortedOption.DESC) => movies.OrderByDescending(x => x.Rating).ToList(),
                (nameof(Movie.ReleaseDate), SortedOption.ASC) => movies.OrderBy(x => x.ReleaseDate).ToList(),
                (nameof(Movie.ReleaseDate), SortedOption.DESC) => movies.OrderByDescending(x => x.ReleaseDate).ToList(),
                _ => movies
            };

            return await Task.FromResult(sortedMovies);
        }


        public async Task<MovieResponse> UpdateMovies(MovieUpdateRequest? movieUpdateRequest)
        {
            if(movieUpdateRequest == null)
                throw new ArgumentNullException(nameof(movieUpdateRequest));
           


            ValidationModel.ValidateModel(movieUpdateRequest);

            var Movie = await _movieRepository.GetMovieByID(movieUpdateRequest.MovieID);
            if (movieUpdateRequest.Poster != null)
            {
                string filename = await _fileServices.UpdateFile(movieUpdateRequest.Poster, movieUpdateRequest.ImageURL);
                movieUpdateRequest.ImageURL = filename;
                Movie.ImageURL = movieUpdateRequest.ImageURL;
            }
            Movie.MovieID = movieUpdateRequest.MovieID;
            Movie.Title = movieUpdateRequest.Title;
            Movie.GenreID = movieUpdateRequest.GenreID;
            Movie.Rating = movieUpdateRequest.Rating;
           

            await _movieRepository.UpdateMovie(Movie);

            return Movie.ToMovieResponse();
        }
    }
}
