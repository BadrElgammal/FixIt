using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Infrastructure.Repositories
{

    public class ReportRepository : GenericRepositoryAsync<Report>, IReportRepository
    {

        private readonly FIXITDbContext _db;

        public ReportRepository(FIXITDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Report>> GetAllReportsAsync()
        {
            return await _db.Reports
                         .Include(r => r.ReporterUser)
                         .Include(r => r.ReportedUser)
                         .Include(r => r.ServiceRequest)
                         .ToListAsync();

        }

        public async Task<Report> GetReportByReportIdAsync(int reportId)
        {
            return await _db.Reports.Where(r => r.ReportId == reportId)
                         .Include(r => r.ReporterUser)
                         .Include(r => r.ReportedUser)
                         .Include(r => r.ServiceRequest)
                         .FirstOrDefaultAsync();

        }
    }
}
