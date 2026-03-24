using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;

namespace FixIt.Service.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _reviewsRepository;

        public ReviewsService(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        public async Task<string> AddReviewsAsync(Review review)
        {
            await _reviewsRepository.AddAsync(review);
            return "success";

        }

        public async Task<string> DeleteReviewAsync(Review review)
        {

            await _reviewsRepository.DeleteAsync(review);
            return "success";
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _reviewsRepository.GetAllReviewsAsync();

        }

        public async Task<List<Review>> GetAllReviewsByWorkerIdAsync(Guid workerId)
        {
            return await _reviewsRepository.GetAllReviewsByWorkerIdAsync(workerId);
        }

        public async Task<Review> GetReviewByIdAsync(int ReviewId)
        {
            return await _reviewsRepository.GetByIdAsync(ReviewId);
        }


    }
}
