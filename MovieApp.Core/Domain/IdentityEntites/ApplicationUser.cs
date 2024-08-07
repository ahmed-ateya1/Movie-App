using Microsoft.AspNetCore.Identity;
using MovieApp.Core.Domain.Entites;
using System;
using System.Collections.Generic;

namespace MovieApp.Core.Domain.IdentityEntites
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Address { get; set; }
        public Guid? FavouriteID { get; set; }
        public virtual Favourite Favourite { get; set; }
    }
}
