using MovieApp.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.DTO
{
    public class GenreResponse
    {
        public Guid GenreID { get; set; }
        public string? GenreName { get; set; }
    }
    public static class GenreExtension
    {
        public static GenreResponse ToGenreResponse(this Genre genre)
        {
            return new GenreResponse()
            {
                GenreID = genre.GenreID,
                GenreName = genre.GenreName
            };
        }
    }
}
