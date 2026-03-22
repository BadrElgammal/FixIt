using FixIt.Domain.Entities;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IFavoritesRepository : IGenericRepositoryAsync<Favorite>
    {
        Task<List<Favorite>> GetAllFavoritesByUserId(object userId);
        Task<Favorite> GetFavoriteByClientIdAndWorkerId(Guid clientId, Guid WorkerId);

    }
}
