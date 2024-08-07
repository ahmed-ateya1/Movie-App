using MovieApp.Core.Domain.Entites;
using System;

namespace MovieApp.Core.DTO
{
    public class FavouriteResponse
    {
        public Guid FavouriteID { get; set; }
        public Guid UserID { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is FavouriteResponse response &&
                   FavouriteID.Equals(response.FavouriteID) &&
                   UserID.Equals(response.UserID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FavouriteID, UserID);
        }
    }

    public static class FavouriteExtension
    {
        public static FavouriteResponse ToFavouriteResponse(this Favourite favourite)
        {
            return new FavouriteResponse()
            {
                FavouriteID = favourite.FavouriteID,
                UserID = favourite.UserID,
            };
        }
    }
}
