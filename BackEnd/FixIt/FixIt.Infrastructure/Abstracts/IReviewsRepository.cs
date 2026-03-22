using FixIt.Domain.Entities;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IReviewsRepository : IGenericRepositoryAsync<Review>
    {
        Task<List<Review>> GetAllReviewsAsync();


    }
}
