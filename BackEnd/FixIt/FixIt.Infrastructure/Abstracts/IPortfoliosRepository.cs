using FixIt.Domain.Entities;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IPortfoliosRepository : IGenericRepositoryAsync<Portfolio>
    {
        Task<List<Portfolio>> GetAllPortfoliosByWorkerId(object workerId);
        Task<List<Portfolio>> GetAllPortfoliosByUserId(Guid userId);
        Task<Portfolio> GetPortfolioByidAsNoTrackingAsync(int portfolioId);


    }
}
