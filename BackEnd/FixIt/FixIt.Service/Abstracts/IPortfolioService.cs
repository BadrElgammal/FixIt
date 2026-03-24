using FixIt.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Abstracts
{
    public interface IPortfolioService
    {

        Task<List<Portfolio>> GetAllPortfoliosByWorkerIdAsync(object workerId);
        Task<List<Portfolio>> GetAllPortfoliosByUserIdAsync(Guid userId);
        Task<string> AddPortfolioAsync(Portfolio portfolio);
        Task<string> DeletePortfolioAsync(Portfolio portfolio);
        Task<string> UpdatePortfolioAsync(Portfolio portfolio);
        Task<Portfolio> GetPortfolioByidAsync(int id);
        public Task<string> AddPortfolioImage(Portfolio portfolio, IFormFile file);




    }
}
