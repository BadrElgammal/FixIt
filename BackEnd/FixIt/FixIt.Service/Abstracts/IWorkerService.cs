using FixIt.Domain.Entities;

namespace FixIt.Service.Abstracts
{
    public interface IWorkerService
    {
        public Task<List<WorkerProfile>> GetAllWorkersAsync();
        public Task<WorkerProfile> GetWorkerById(Guid id);

        public Task<string> DeleteWorkerAsync(Guid id);
        public Task<string> EditeWorkerAsync(WorkerProfile worker, User user);
        public Task<string> ChangeWorkerPasswordAsync(WorkerProfile worker, string passward);


    }
}
