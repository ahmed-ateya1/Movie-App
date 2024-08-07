using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Domain.Entites
{
    public class Genre
    {
        public Guid GenreID { get; set; }
        public string? GenreName { get; set; }
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
