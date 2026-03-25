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

        public async Task<Portfolio> GetPortfolioByidAsNoTrackingAsync(int portfolioId)
        {
            return await _db.Portfolios
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PortfolioId == portfolioId);
        }
    }


}

