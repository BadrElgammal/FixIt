using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Services
{
    public class PortfoliosService : IPortfolioService
    {
        private readonly IPortfoliosRepository _portfoliosRepo;
        private readonly IFileService _fileService;



        public PortfoliosService(IPortfoliosRepository portfoliosRepo, IFileService fileService)
        {
            _portfoliosRepo = portfoliosRepo;
            _fileService = fileService;
        }

        public async Task<string> AddPortfolioAsync(Portfolio portfolio, IFormFile file)
        {
            //await _portfoliosRepo.AddAsync(portfolio);
            //return "success";


            var ImgUrl = await _fileService.UploadImage("Portfolios", file);
            portfolio.ImgUrl = ImgUrl;

            switch (ImgUrl)
            {
                case "No Image !!": return "No Image !!";
                case "Feild to Uplaod !!": return "Feild to Uplaod !!";
            }

            try
            {

                await _portfoliosRepo.AddAsync(portfolio);
                return "success";

            }
            catch (Exception)
            {

                return "FaildinAdd";
            }


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
            // return await _portfoliosRepo.GetByIdAsync(id);
            return await _portfoliosRepo.GetPortfolioByidAsNoTrackingAsync(id);
        }

        public async Task<string> UpdatePortfolioAsync(Portfolio portfolio, IFormFile? file)
        {
            //await _portfoliosRepo.UpdateAsync(portfolio);
            //return "success";

            var ImgUrl = await _fileService.UploadImage("Portfolios", file);
            portfolio.ImgUrl = ImgUrl;

            switch (ImgUrl)
            {
                case "No Image !!": return "No Image !!";
                case "Feild to Uplaod !!": return "Feild to Uplaod !!";
            }

            try
            {
                await _portfoliosRepo.UpdateAsync(portfolio);
                return "success";

            }
            catch (Exception)
            {

                return "FaildinAdd";
            }



        }




    }
}
