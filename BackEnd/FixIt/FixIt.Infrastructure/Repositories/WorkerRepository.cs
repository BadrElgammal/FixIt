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
                                 .Where(w => w.WorkerId == WorkerId)
                                 .FirstOrDefaultAsync();

        }

        public async Task<Guid> GetWorkerIdByUserIdAsync(Guid UserId)
        {
            return _context.WorkerProfiles.Where(w => w.User.UserId == UserId).Select(s => s.WorkerId).FirstOrDefault();

        }




        #endregion



    }
}
