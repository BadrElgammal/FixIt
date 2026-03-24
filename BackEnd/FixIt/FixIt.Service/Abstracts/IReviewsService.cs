using FixIt.Domain.Entities;

namespace FixIt.Service.Abstracts
{
    public interface IReviewsService
    {
        Task<List<Review>> GetAllReviewsAsync();
        Task<List<Review>> GetAllReviewsByWorkerIdAsync(Guid workerId);
        Task<string> AddReviewsAsync(Review review);
        Task<Review> GetReviewByIdAsync(int ReviewId);
        Task<string> DeleteReviewAsync(Review review);


    }
}
