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

        public Task<List<Review>> GetAllReviewsByWorkerIdAsync(Guid workerId)
        {
            return _dbContext.Reviews.Include(r => r.Reviewer).Include(r => r.Request).Include(r => r.ReviewedWorker)
                                     .Where(r => r.ReviewedWorkerId == workerId)
                                     .ToListAsync();
        }



        public async Task<WorkerProfile> GetWorkerByWorkerIdAsync(Guid workerId)
        {
            //return _dbContext.WorkerProfiles.Where(w => w.WorkerId == workerId)
            //                                .Include(w => w.User)
            //                                .FirstOrDefault();

            return await _dbContext.WorkerProfiles
                                   .Where(w => w.WorkerId == workerId)
                                   .Include(w => w.User)
                                   .Include(w => w.Reviews)
                                   .ThenInclude(r => r.Reviewer)
                                   .FirstOrDefaultAsync();

        }

        public async Task<Guid> GetWorkerIdByUserIdAsync(Guid userId)
        {
            return _dbContext.WorkerProfiles.Where(w => w.UserId == userId)
                                            .Select(w => w.WorkerId)
                                            .FirstOrDefault();

        }
    }
}
