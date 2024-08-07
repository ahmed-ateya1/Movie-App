using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.RepositoryContracts;
using MovieApp.Infrastructure.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure.Repositories
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private readonly AppDbContext _db;

        public FavouriteRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Favourite> CreateFavourite(Favourite favourite)
        {
            await _db.Favorites.AddAsync(favourite);
            await _db.SaveChangesAsync();
            return favourite;
        }

        public async Task<bool> DeleteFavourite(Guid favouriteID)
        {
            var favourite = await GetFavouriteByFavID(favouriteID);
            if (favourite == null)
                return false;

            _db.Favorites.Remove(favourite);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Favourite>> GetAllFavourites()
        {
            return await _db.Favorites.ToListAsync();
        }

        public async Task<Favourite> GetFavouriteByFavID(Guid favouriteID)
        {
            return await _db.Favorites.FirstOrDefaultAsync(x => x.FavouriteID == favouriteID);
        }

        public async Task<Favourite> GetFavouriteByUserID(Guid userID)
        {
            return await _db.Favorites.FirstOrDefaultAsync(x => x.UserID == userID);
        }

        public async Task<Favourite> UpdateFavourite(Favourite favourite)
        {
            var favouriteOld = await GetFavouriteByFavID(favourite.FavouriteID);
            if (favouriteOld == null)
                return null;

            favouriteOld.UserID = favourite.UserID;

            return favouriteOld;
        }
    }
}
