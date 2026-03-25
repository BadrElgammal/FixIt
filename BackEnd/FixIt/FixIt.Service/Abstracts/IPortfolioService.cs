using FixIt.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Abstracts
{
    public interface IPortfolioService
    {

        Task<List<Portfolio>> GetAllPortfoliosByWorkerIdAsync(object workerId);
        Task<List<Portfolio>> GetAllPortfoliosByUserIdAsync(Guid userId);
        Task<string> AddPortfolioAsync(Portfolio portfolio, IFormFile file);
        Task<string> DeletePortfolioAsync(Portfolio portfolio);
        Task<string> UpdatePortfolioAsync(Portfolio portfolio, IFormFile? file);
        Task<Portfolio> GetPortfolioByidAsync(int id);




    }
}
