using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Domain.Entites
{
    public class MovieFavourite
    {
        public Guid MovieID { get; set; }
        public Guid FavouriteID { get; set; }

    }
}
