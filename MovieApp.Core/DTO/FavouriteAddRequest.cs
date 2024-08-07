using MovieApp.Core.Domain.Entites;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Core.DTO
{
    public class FavouriteAddRequest
    {
        [Required(ErrorMessage = "UserID can't be blank")]
        public Guid UserID { get; set; }

        public Favourite ToFavourite()
        {
            return new Favourite()
            {
                UserID = UserID,
            };
        }
    }
}
