using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.IdentityEntites;
using MovieApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Core.ServicesContracts
{
    public interface IFavouriteServices
    {
        Task<FavouriteResponse> CreateFavourite(FavouriteAddRequest? favouriteAddRequest);
        Task<bool> DeleteFavourite(Guid? favouriteID);
        Task<FavouriteResponse> GetFavouriteByFavID(Guid? favouriteID);
        Task<bool> HaveFavourite(Guid? userID);
        Task<FavouriteResponse> GetFavouriteByUserId(Guid? userID);
    }
}
