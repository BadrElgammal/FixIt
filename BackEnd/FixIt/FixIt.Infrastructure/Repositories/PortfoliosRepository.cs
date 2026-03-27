using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Infrastructure.Repositories
{
    public class PortfoliosRepository : GenericRepositoryAsync<Portfolio>, IPortfoliosRepository
    {

        private readonly FIXITDbContext _db;


        public PortfoliosRepository(FIXITDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Portfolio>> GetAllPortfoliosByUserId(Guid userId)
        {
            return await _db.Portfolios.Include(p => p.WorkerProfile)
                                        .Where(p => p.WorkerProfile.UserId == userId)
                                        .ToListAsync();

        }

        public async Task<List<Portfolio>> GetAllPortfoliosByWorkerId(object workerId)
        {
            return await _db.Portfolios.Include(p => p.WorkerProfile)
                                        .Where(w => w.WorkerProfileId == (Guid)workerId)
                                        .ToListAsync();


        }

        public async Task<Portfolio> GetPortfolioByidAsync(int portfolioId)
        {

            return await _db.Portfolios
                .FirstOrDefaultAsync(p => p.PortfolioId == portfolioId);

        }

        public async Task<WorkerProfile> GetWorkerByWorkerId(Guid workerId)
        {


            return await _db.WorkerProfiles.Where(w => w.WorkerId == workerId)
                                            .Include(w => w.User)
                                            .Include(w => w.Portfolios)
                                            .FirstOrDefaultAsync();
        }

        public async Task<Guid> GetWorkerIdByUserId(Guid userId)
        {
            return _db.WorkerProfiles.Where(w => w.UserId == userId).Select(w => w.WorkerId).FirstOrDefault();
            //return _db.Portfolios.Where(s => s.WorkerProfile.UserId == userId)
            //    .Select(s => s.WorkerProfileId)
            //    .FirstOrDefault();

        }
    }


}

