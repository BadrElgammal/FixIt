using FixIt.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace FixIt.Service.Abstracts
{
    public interface IServiceRequestService
    {
        Task<string> CreateServiceRequest(ServiceRequest serviceRequest, IFormFile? file);
        IEnumerable<ServiceRequest> Find(Expression<Func<ServiceRequest, bool>> predicate);
        Guid GetWorkerIdByUserId(Guid userId);
        Task<String> EditServiceRequestAsync(ServiceRequest serviceRequest, IFormFile? file);
        Task<String> EditServiceRequestAsync(ServiceRequest serviceRequest);
        Task<Wallet> GetWalletByClientId(Guid clientId);
        Task<Wallet> GetWalletByWorkerId(Guid workerId);
        Task<string> EditWallet(Wallet wallet);
        Task<string> CreateTransaction(Transaction transaction);
        Task<ServiceRequest> GetServiceRequestWithAllData(object serviceId);
        Task<List<ServiceRequest>> GetAllServiceRequestWithAllDataByClientId(object ClientId);
        Task<List<ServiceRequest>> GetAllServiceRequestWithAllDataByWorkerId(object WorkerId);


    }
}
