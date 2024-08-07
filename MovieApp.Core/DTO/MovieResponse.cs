using MovieApp.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.DTO
{
    public class MovieResponse
    {
        public Guid MovieID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double? Rating { get; set; }
        public string? ImageURL { get; set; }
        public Guid? GenreID { get; set; }
        public string? GenreName { get; set; }
        public double? ProducedSince { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MovieResponse response &&
                   MovieID.Equals(response.MovieID) &&
                   Title == response.Title &&
                   Description == response.Description &&
                   ReleaseDate == response.ReleaseDate &&
                   Rating == response.Rating &&
                   ImageURL == response.ImageURL &&
                   EqualityComparer<Guid?>.Default.Equals(GenreID, response.GenreID) &&
                   GenreName == response.GenreName &&
                   ProducedSince == response.ProducedSince;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(MovieID);
            hash.Add(Title);
            hash.Add(Description);
            hash.Add(ReleaseDate);
            hash.Add(Rating);
            hash.Add(ImageURL);
            hash.Add(GenreID);
            hash.Add(GenreName);
            hash.Add(ProducedSince);
            return hash.ToHashCode();
        }
    }
    public static class MovieExtension
    {
        public static MovieResponse ToMovieResponse(this Movie movie)
        {
            return new MovieResponse
            {
                MovieID = movie.MovieID,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                ImageURL = movie.ImageURL,
                GenreID = movie.GenreID,
                Title = movie.Title,
                GenreName = movie.Genre?.GenreName,
                ProducedSince = (movie.ReleaseDate.HasValue) ? Math.Round((DateTime.Now - movie.ReleaseDate.Value).TotalDays / 365.25) : null,
            };
        }
    }
}
