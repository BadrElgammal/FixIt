using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Infrastructure.Repositories
{
    public class WorkerRepository : GenericRepositoryAsync<WorkerProfile>, IWorkerRepository
    {

        #region Fields
        private readonly DbSet<WorkerProfile> _Workers;    //WorkersRepo

        #endregion

        #region Ctors

        public WorkerRepository(FIXITDbContext db) : base(db)
        {
            _Workers = db.Set<WorkerProfile>();
        }

        #endregion

        #region Methods/functions Handel

        public async Task<List<WorkerProfile>> GetAllWorkersAsync()
        {
            return await _Workers.Include(w => w.User)
                        .Include(w => w.Category)
                        .ToListAsync();
        }

        public async Task<WorkerProfile> GetWorkerByIdAsync(object id)
        {
            return await _Workers.Include(w => w.User)
                              .Include(w => w.Category)
                              .Where(w => w.UserId == (Guid)id)
                              .FirstOrDefaultAsync();

        }


        #endregion



    }
}
