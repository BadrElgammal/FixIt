using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Infrastructure.Repositories
{
    public class WorkerRepository : GenericRepositoryAsync<WorkerProfile>, IWorkerRepository
    {

        #region Fields

        private readonly FIXITDbContext _context;

        #endregion

        #region Ctors



        public WorkerRepository(FIXITDbContext context) : base(context)
        {
            _context = context;
        }





        #endregion

        #region Methods/functions Handel

        public async Task<List<WorkerProfile>> GetAllWorkersAsync()
        {
            return await _dbContext.WorkerProfiles.Include(w => w.User)
                        .Include(w => w.Category)
                        .ToListAsync();
        }


        public async Task<WorkerProfile> GetWorkerByIdAsync(object id)
        {
            return await _dbContext.WorkerProfiles.Include(w => w.User)
                              .Include(w => w.Category)
                              .Where(w => w.UserId == (Guid)id)
                              .FirstOrDefaultAsync();

        }

        public async Task<WorkerProfile> GetWorkerByWorkerIdAsync(Guid WorkerId)
        {

            return await _dbContext.WorkerProfiles.Include(w => w.User)
                                 .Include(w => w.Category)
                                 .Include(w => w.Reviews)
                                 .Where(w => w.WorkerId == WorkerId)
                                 .FirstOrDefaultAsync();

        }

        public async Task<Guid> GetWorkerIdByUserIdAsync(Guid UserId)
        {
            return await _context.WorkerProfiles.Where(w => w.User.UserId == UserId)
                .Select(s => s.WorkerId)
                .FirstOrDefaultAsync();

        }


        //get LAst => 
        public async Task<List<ServiceRequest>> GetLastServicesRequestForWorkerAsync(Guid WorkerId, int? NumberOfServices = null)
        {
            var allServices = _context.ServiceRequests.Where(s => s.WorkerId == WorkerId)
                                           .Include(s => s.Client)
                                           .Include(s => s.Worker)
                                           .OrderByDescending(s => s.RequestDate);


            if (NumberOfServices == null)
                return await allServices.ToListAsync();
            else
                return await allServices.Take(NumberOfServices.Value).ToListAsync();

        }


        public async Task<List<ChatRoom>> GetLastMessagesRForWorkerAsync(Guid UserId, int? SelectedNumber = null)
        {

            var allLastMessages = _context.ChatRooms.Where(s => s.TargetUserId == UserId || s.CurrentUserId == UserId)
                                       .Include(s => s.TargetUser)
                                       .Include(s => s.CurrentUser)
                                       .OrderByDescending(s => s.LastMessageAt);


            if (SelectedNumber == null)
                return await allLastMessages.ToListAsync();
            else
                return await allLastMessages.Take(SelectedNumber.Value).ToListAsync();

        }

        public async Task<List<Review>> GetLastReviewsForWorkerAsync(Guid WorkerId, int? SelectedNumber = null)
        {

            var allReviews = _context.Reviews.Where(s => s.ReviewedWorkerId == WorkerId)
                                      .Include(s => s.Reviewer)
                                      .Include(s => s.ReviewedWorker)
                                      .OrderByDescending(s => s.CreatedAt);


            if (SelectedNumber == null)
                return await allReviews.ToListAsync();
            else
                return await allReviews.Take(SelectedNumber.Value).ToListAsync();

        }

        public async Task<int> GetNumberOfPortfoliosForWorkerAsync(Guid WorkerId)
        {
            return await _context.Portfolios.Where(p => p.WorkerProfileId == WorkerId)
                 .CountAsync();
        }

        public async Task<int> GetTotalNumberOfReportsForWorkerAsync(Guid userId)
        {
            return await _context.Reports.Where(p => p.ReportedUserId == userId)
                .CountAsync();
        }


        #endregion



    }
}
