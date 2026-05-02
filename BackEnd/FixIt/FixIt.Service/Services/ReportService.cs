using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;

namespace FixIt.Service.Services
{
    public class ReportService : GenericService<Report>, IReportService
    {
        private readonly IReportRepository _reportRepo;

        public ReportService(IReportRepository reportRepo) : base(reportRepo)
        {
            _reportRepo = reportRepo;
        }


        public async Task<string> AddReportAsync(Report report)
        {
            await _reportRepo.AddAsync(report);
            return "success";
        }

        public async Task<string> UpdateReportAsync(Report report)
        {
            await _reportRepo.UpdateAsync(report);
            return "success";
        }

        public async Task<List<Report>> GetAllReportsAsync()
        {
            return await _reportRepo.GetAllReportsAsync();
        }

        public async Task<Report> GetReportByReportIdAsync(int id)
        {
            return await _reportRepo.GetReportByReportIdAsync(id);
        }

    }
}
