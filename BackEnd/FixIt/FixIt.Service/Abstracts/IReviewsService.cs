using FixIt.Domain.Entities;

namespace FixIt.Service.Abstracts
{
    public interface IReviewsService
    {
        public Task<Guid> GetWorkerIdByUserId(Guid userId);
        Task<List<Review>> GetAllReviewsAsync();
        Task<List<Review>> GetAllReviewsByWorkerIdAsync(Guid workerId);
        Task<string> AddReviewsAsync(Review review);
        Task<Review> GetReviewByIdAsync(int ReviewId);
        Task<string> DeleteReviewAsync(Review review);


    }
}
