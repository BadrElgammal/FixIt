using FixIt.Domain.Entities;

namespace FixIt.Service.Abstracts
{
    public interface IReportService : IService<Report>
    {

        Task<string> AddReportAsync(Report report);
        Task<List<Report>> GetAllReportsAsync();
        Task<Report> GetReportByReportIdAsync(int reportId);

    }
}
