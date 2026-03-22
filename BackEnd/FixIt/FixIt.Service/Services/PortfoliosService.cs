using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;

namespace FixIt.Service.Services
{
    public class PortfoliosService : IPortfolioService
    {
        private readonly IPortfoliosRepository _portfoliosRepo;



        public PortfoliosService(IPortfoliosRepository portfoliosRepo)
        {
            _portfoliosRepo = portfoliosRepo;

        }

        public async Task<string> AddPortfolioAsync(Portfolio portfolio)
        {
            await _portfoliosRepo.AddAsync(portfolio);
            return "success";
        }

        public async Task<string> DeletePortfolioAsync(Portfolio portfolio)
        {
            await _portfoliosRepo.DeleteAsync(portfolio);
            return "success";
        }

        public async Task<List<Portfolio>> GetAllPortfoliosByUserIdAsync(Guid userId)
        {
            return await _portfoliosRepo.GetAllPortfoliosByUserId(userId);
        }

        public async Task<List<Portfolio>> GetAllPortfoliosByWorkerIdAsync(object workerId)
        {
            var portfoliosList = await _portfoliosRepo.GetAllPortfoliosByWorkerId((Guid)workerId);

            return portfoliosList;
        }

        public async Task<Portfolio> GetPortfolioByidAsync(int id)
        {
            return await _portfoliosRepo.GetByIdAsync(id);
        }

        public async Task<string> UpdatePortfolioAsync(Portfolio portfolio)
        {
            await _portfoliosRepo.UpdateAsync(portfolio);
            return "success";
        }
    }
}
