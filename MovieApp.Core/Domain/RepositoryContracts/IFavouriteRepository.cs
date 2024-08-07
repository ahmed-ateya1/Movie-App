using MovieApp.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Core.Domain.RepositoryContracts
{
    public interface IFavouriteRepository
    {
        Task<Favourite> CreateFavourite(Favourite favourite);
        Task<Favourite> UpdateFavourite(Favourite favourite);
        Task<bool> DeleteFavourite(Guid favouriteID);
        Task<Favourite> GetFavouriteByFavID(Guid favouriteID);
        Task<Favourite> GetFavouriteByUserID(Guid userID);
        Task<IEnumerable<Favourite>> GetAllFavourites();
    }
}
