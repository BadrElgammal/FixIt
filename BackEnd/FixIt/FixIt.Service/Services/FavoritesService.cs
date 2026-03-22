using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;

namespace FixIt.Service.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoritesRepository _favoritesRepository;

        public FavoritesService(IFavoritesRepository favoritesRepository)
        {
            _favoritesRepository = favoritesRepository;
        }

        public async Task<string> AddFavorite(Favorite favorite)
        {
            await _favoritesRepository.AddAsync(favorite);
            return "success";
        }

        public async Task<string> DeleteFavorite(Favorite favorite)
        {
            await _favoritesRepository.DeleteAsync(favorite);
            return "success";
        }

        public async Task<List<Favorite>> GetAllFavoritesByUserId(object userId)
        {
            return await _favoritesRepository.GetAllFavoritesByUserId(userId);
        }

        public async Task<Favorite> GetFavoriteByClientIdAndWorkerId(Guid ClientId, Guid workerId)
        {
            return await _favoritesRepository.GetFavoriteByClientIdAndWorkerId(ClientId, workerId);
        }


    }
}
