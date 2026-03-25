using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace FixIt.Service.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly IFileService _fileService;



        public ServiceRequestService(IServiceRequestRepository serviceRequestRepository, IFileService fileService)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _fileService = fileService;
        }
        public async Task<string> CreateServiceRequest(ServiceRequest serviceRequest, IFormFile? file)
        {

            //await _serviceRequestRepository.AddAsync(serviceRequest);
            //return "success";



            var ImgUrl = await _fileService.UploadImage("RequestedServices", file);
            serviceRequest.RequestedImgUrl = ImgUrl;

            switch (ImgUrl)
            {
                case "No Image !!": return "No Image !!";
                case "Feild to Uplaod !!": return "Feild to Uplaod !!";
            }

            try
            {

                await _serviceRequestRepository.AddAsync(serviceRequest);
                return "success";
            }
            catch (Exception)
            {

                return "FaildinAdd";
            }



        }

        public async Task<string> CreateTransaction(Transaction transaction)
        {
            return await _serviceRequestRepository.CreateTransaction(transaction);
        }

        public async Task<string> EditServiceRequestAsync(ServiceRequest serviceRequest, IFormFile? file)
        {
            //await _serviceRequestRepository.UpdateAsync(serviceRequest);
            //return "success";



            var ImgUrl = await _fileService.UploadImage("SubmitedServices", file);
            serviceRequest.SubmitedImgUrl = ImgUrl;

            switch (ImgUrl)
            {
                case "No Image !!": return "No Image !!";
                case "Feild to Uplaod !!": return "Feild to Uplaod !!";
            }

            try
            {

                await _serviceRequestRepository.UpdateAsync(serviceRequest);
                return "success";
            }
            catch (Exception)
            {

                return "FaildinAdd";
            }


        }

        public async Task<string> EditServiceRequestAsync(ServiceRequest serviceRequest)
        {
            await _serviceRequestRepository.UpdateAsync(serviceRequest);
            return "success";

        }

        public async Task<string> EditWallet(Wallet wallet)
        {
            return await _serviceRequestRepository.EditWallet(wallet);
        }

        public IEnumerable<ServiceRequest> Find(Expression<Func<ServiceRequest, bool>> predicate)
        {
            return _serviceRequestRepository.Find(predicate);
        }

        public async Task<List<ServiceRequest>> GetAllServiceRequestWithAllDataByClientId(object ClientId)
        {
            return await _serviceRequestRepository.GetAllServiceRequestWithAllDataByClientId(ClientId);
        }

        public async Task<List<ServiceRequest>> GetAllServiceRequestWithAllDataByWorkerId(object WorkerId)
        {
            return await _serviceRequestRepository.GetAllServiceRequestWithAllDataByWorkerId(WorkerId);
        }

        public async Task<ServiceRequest> GetServiceRequestWithAllData(object serviceId)
        {
            return await _serviceRequestRepository.GetServiceRequestWithAllData(serviceId);
        }

        public async Task<Wallet> GetWalletByClientId(Guid clientId)
        {
            return await _serviceRequestRepository.GetWalletByClientId(clientId);
        }

        public async Task<Wallet> GetWalletByWorkerId(Guid workerId)
        {
            return await _serviceRequestRepository.GetWalletByWorkerId(workerId);
        }

        public Guid GetWorkerIdByUserId(Guid userId)
        {
            return _serviceRequestRepository.GetWorkerIdByUserId(userId);
        }
    }
}
