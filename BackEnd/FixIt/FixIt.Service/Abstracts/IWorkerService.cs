using FixIt.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FixIt.Service.Abstracts
{
    public interface IWorkerService
    {
        public Task<List<WorkerProfile>> GetAllWorkersAsync();
        public Task<WorkerProfile> GetWorkerById(Guid id);
        public Task<WorkerProfile> GetWorkerByWorkerId(Guid id);
        public Task<Guid> GetWorkerIdByUserId(Guid id);

        public Task<string> DeleteWorkerAsync(Guid id);
        public Task<string> EditeWorkerAsync(WorkerProfile worker, User user);
        public Task<string> EditeWorkerAsync(WorkerProfile worker);
        public Task<string> ChangeWorkerPasswordAsync(WorkerProfile worker, string passward);
        public Task<string> ChangeWorkerImage(User user, IFormFile file);
        public Task<User> GetUserByUserId(Guid userId);

        public IQueryable<WorkerProfile> GetAllWorkersPaginated();
        public IQueryable<WorkerProfile> GetAllWorkersPaginatedWithFiltaration(string search, string address, bool? isAvilable);


        //last 5 => 
        public Task<List<ServiceRequest>> GetLastServicesForWorkerAsync(Guid workerId, int? NumberofServices);
        public Task<List<ChatRoom>> GetLastMessagessForWorkerAsync(Guid userId, int? SelectedNumber);
        public Task<List<Review>> GetLastReviewsForWorkerAsync(Guid workerId, int? SelectedNumber);
        public Task<int> GetTotalNumberOfPortfoliosForWorkerAsync(Guid workerId);
        public Task<int> GetTotalNumberOfReportsForWorkerAsync(Guid userID);



    }
}
