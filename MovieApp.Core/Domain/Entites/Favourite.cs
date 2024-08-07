using MovieApp.Core.Domain.IdentityEntites;
using System;
using System.Collections.Generic;

namespace MovieApp.Core.Domain.Entites
{
    public class Favourite
    {
        public Guid FavouriteID { get; set; }
        public Guid UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public virtual ICollection<MovieFavourite> MovieFavourites { get; set; } = new List<MovieFavourite>();
    }
}
