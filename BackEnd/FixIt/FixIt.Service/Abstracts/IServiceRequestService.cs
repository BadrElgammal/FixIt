using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Abstracts
{
    public interface IServiceRequestService
    {
        Task<string> CreateServiceRequest(ServiceRequest serviceRequest);
        IEnumerable<ServiceRequest> Find(Expression<Func<ServiceRequest, bool>> predicate);
        Guid GetWorkerIdByUserId(Guid userId);
        Task<String> EditServiceRequestAsync(ServiceRequest serviceRequest);
        Task<Wallet> GetWalletByClientId(Guid clientId);
        Task<Wallet> GetWalletByWorkerId(Guid workerId);
        Task<string> EditWallet(Wallet wallet);
        Task<string> CreateTransaction(Transaction transaction);

    }
}
