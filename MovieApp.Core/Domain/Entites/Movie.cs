using MovieApp.Core.Domain.IdentityEntites;
using System;
using System.Collections.Generic;

namespace MovieApp.Core.Domain.Entites
{
    public class Movie
    {
        public Guid MovieID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double? Rating { get; set; }
        public string? ImageURL { get; set; }
        public Guid? GenreID { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public virtual ICollection<MovieFavourite> MovieFavourites { get; set; } = new List<MovieFavourite>();

    }
}
