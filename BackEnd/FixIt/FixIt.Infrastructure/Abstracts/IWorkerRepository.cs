using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IWorkerRepository:IGenericRepositoryAsync<WorkerProfile>
    {
        public Task<List<WorkerProfile>> GetAllWorkersAsync();
        public Task<WorkerProfile> GetWorkerByIdAsync(object id);


    }
}
