using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.RepositoryContracts;
using MovieApp.Core.DTO;
using MovieApp.Core.Helper;
using MovieApp.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Core.Services
{
    public class FavouriteServices : IFavouriteServices
    {
        private readonly IFavouriteRepository _favouriteRepository;

        public FavouriteServices(IFavouriteRepository favouriteRepository)
        {
            _favouriteRepository = favouriteRepository;
        }

        public async Task<FavouriteResponse> CreateFavourite(FavouriteAddRequest? favouriteAddRequest)
        {
            if(favouriteAddRequest == null)
                throw new ArgumentNullException(nameof(favouriteAddRequest));

            ValidationModel.ValidateModel(favouriteAddRequest);

            var favourite = favouriteAddRequest.ToFavourite();

            favourite.FavouriteID = Guid.NewGuid();

            await _favouriteRepository.CreateFavourite(favourite);

            return favourite.ToFavouriteResponse(); 
        }

        public async Task<bool> DeleteFavourite(Guid? favouriteID)
        {
            if(favouriteID == null)
                throw new ArgumentNullException();

            var favouriteResponse = await GetFavouriteByFavID(favouriteID);

            if(favouriteResponse == null)
                return false;

            return await _favouriteRepository.DeleteFavourite(favouriteID.Value);
        }

        public async Task<FavouriteResponse> GetFavouriteByFavID(Guid? favouriteID)
        {
            if (favouriteID == null)
                throw new ArgumentNullException();

            var favourite =await _favouriteRepository
                .GetFavouriteByFavID(favouriteID.Value);

            return favourite.ToFavouriteResponse();
        }

        public async Task<FavouriteResponse> GetFavouriteByUserId(Guid? userID)
        {
            if (userID == null)
                throw new ArgumentNullException();

            var favourite = await _favouriteRepository.GetFavouriteByUserID(userID.Value);

            return favourite.ToFavouriteResponse();
        }

        public async Task<bool> HaveFavourite(Guid? userID)
        {
            if(!userID.HasValue)
                throw new ArgumentNullException();

            var favourite = await _favouriteRepository
                .GetFavouriteByUserID(userID.Value);

            if (favourite == null)
                return false;
            return true;
        }
    }
}
