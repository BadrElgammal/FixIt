using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Infrastructure.Repositories
{
    public class ServiceRequestRepository : GenericRepositoryAsync<ServiceRequest> , IServiceRequestRepository
    {
        private readonly FIXITDbContext _dbContext;

        public ServiceRequestRepository(FIXITDbContext dbContext): base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateTransaction(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return "success";
        }

        public async Task<string> EditWallet(Wallet wallet)
        {
            _dbContext.Wallets.Update(wallet);
            _dbContext.SaveChanges();
            return "success";
        }

        public async Task<Wallet> GetWalletByClientId(Guid clientId)
        {
            return  _dbContext.Wallets.Where(w => w.UserId == clientId).FirstOrDefault();
        }

        public async Task<Wallet> GetWalletByWorkerId(Guid workerId)
        {
            var worker = _dbContext.WorkerProfiles.Include(w => w.User).Where(w => w.WorkerId == workerId).FirstOrDefault();
            return  _dbContext.Wallets.Where(w => w.UserId == worker.UserId).FirstOrDefault();
        }

        public Guid GetWorkerIdByUserId(Guid userId)
        {
            return  _dbContext.ServiceRequests.Where(s => s.Worker.UserId == userId).Select(s => s.WorkerId).FirstOrDefault();
        }
    }
}
