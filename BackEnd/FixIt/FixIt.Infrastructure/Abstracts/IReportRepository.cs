using FixIt.Domain.Entities;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IReportRepository : IGenericRepositoryAsync<Report>
    {
        Task<List<Report>> GetAllReportsAsync();
        Task<Report> GetReportByReportIdAsync(int reportId);

    }
}
