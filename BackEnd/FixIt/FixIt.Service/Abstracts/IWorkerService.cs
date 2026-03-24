using FixIt.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Abstracts
{
    public interface IWorkerService
    {
        public Task<List<WorkerProfile>> GetAllWorkersAsync();
        public Task<WorkerProfile> GetWorkerById(Guid id);
        public Task<WorkerProfile> GetWorkerByWorkerId(Guid id);

        public Task<string> DeleteWorkerAsync(Guid id);
        public Task<string> EditeWorkerAsync(WorkerProfile worker, User user);
        public Task<string> EditeWorkerAsync(WorkerProfile worker);
        public Task<string> ChangeWorkerPasswordAsync(WorkerProfile worker, string passward);
        public Task<string> ChangeWorkerImage(User user, IFormFile file);
        public Task<User> GetUserByUserId(Guid userId);


    }
}
