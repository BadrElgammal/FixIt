using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IServiceRequestRepository : IGenericRepositoryAsync<ServiceRequest>
    {
       Guid GetWorkerIdByUserId(Guid userId);
        Task<Wallet> GetWalletByClientId(Guid clientId);
        Task<Wallet> GetWalletByWorkerId(Guid workerId);

        Task<string> EditWallet(Wallet wallet);
        Task<string> CreateTransaction(Transaction transaction);
        Task<ServiceRequest> GetServiceRequestWithAllData(object serviceId);
        Task<List<ServiceRequest>> GetAllServiceRequestWithAllDataByClientId(object ClientId);
        Task<List<ServiceRequest>> GetAllServiceRequestWithAllDataByWorkerId(object WorkerId);
    }
}
