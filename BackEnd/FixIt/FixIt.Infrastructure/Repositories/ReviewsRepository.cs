using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Infrastructure.Repositories
{
    public class ReviewsRepository : GenericRepositoryAsync<Review>, IReviewsRepository
    {
        private readonly FIXITDbContext _context;
        public ReviewsRepository(FIXITDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public Task<List<Review>> GetAllReviewsAsync()
        {
            return _dbContext.Reviews.Include(r => r.Reviewer)
                                     .Include(r => r.Request)
                                     .Include(r => r.ReviewedWorker)
                                     .ToListAsync();
        }
    }
}
