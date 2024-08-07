using MovieApp.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.DTO
{
    public class GenreUpdateRequest
    {
        public Guid GenreID { get; set; }
        [Required(ErrorMessage = "Genre name can't be blank")]
        public string? GenreName { get; set; }

        public Genre ToGenre()
        {
            return new Genre() { GenreName = GenreName };
        }
    }
}
