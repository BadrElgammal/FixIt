using FixIt.Domain.Entities;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IWorkerRepository : IGenericRepositoryAsync<WorkerProfile>
    {
        public Task<List<WorkerProfile>> GetAllWorkersAsync();
        public Task<WorkerProfile> GetWorkerByIdAsync(object id);
        public Task<WorkerProfile> GetWorkerByWorkerIdAsync(Guid WorkerId);
        public Task<Guid> GetWorkerIdByUserIdAsync(Guid UserId);


    }
}
