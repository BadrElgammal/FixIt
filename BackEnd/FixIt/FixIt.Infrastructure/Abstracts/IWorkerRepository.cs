using FixIt.Domain.Entities;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IWorkerRepository : IGenericRepositoryAsync<WorkerProfile>
    {
        public Task<List<WorkerProfile>> GetAllWorkersAsync();
        public Task<WorkerProfile> GetWorkerByIdAsync(object id);
        public Task<WorkerProfile> GetWorkerByWorkerIdAsync(Guid WorkerId);
        public Task<Guid> GetWorkerIdByUserIdAsync(Guid UserId);

        public Task<List<ServiceRequest>> GetLastServicesRequestForWorkerAsync(Guid WorkerId, int? NumberOfServices = null);
        public Task<List<ChatRoom>> GetLastMessagesRForWorkerAsync(Guid userId, int? SelectedNumber = null);
        public Task<List<Review>> GetLastReviewsForWorkerAsync(Guid WorkerId, int? SelectedNumber = null);
        public Task<int> GetNumberOfPortfoliosForWorkerAsync(Guid WorkerId);
        public Task<int> GetTotalNumberOfReportsForWorkerAsync(Guid WorkerId);


    }
}
