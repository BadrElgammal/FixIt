using FixIt.Domain.Entities;

namespace FixIt.Service.Abstracts
{
    public interface IFavoritesService
    {
        Task<List<Favorite>> GetAllFavoritesByUserId(object userId);
        Task<Favorite> GetFavoriteByClientIdAndWorkerId(Guid ClientId, Guid workerId);
        Task<string> DeleteFavorite(Favorite favorite);
        Task<string> AddFavorite(Favorite favorite);

    }
}
