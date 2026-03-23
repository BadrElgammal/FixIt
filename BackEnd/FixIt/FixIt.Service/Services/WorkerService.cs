using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;

namespace FixIt.Service.Services
{
    public class WorkerService : IWorkerService
    {

        #region Fields
        private readonly IWorkerRepository _WorkerRepo;
        private readonly IGenericRepositoryAsync<Wallet> _walletRepo;
        private readonly IGenericRepositoryAsync<User> _userRepo;


        #endregion

        #region Ctors

        public WorkerService(IWorkerRepository WorkerRepo, IGenericRepositoryAsync<Wallet> walletRepo
            , IGenericRepositoryAsync<User> userRepo)
        {
            this._WorkerRepo = WorkerRepo;
            _walletRepo = walletRepo;
            _userRepo = userRepo;
        }


        #endregion





        #region Methods/functions Handel


        public async Task<WorkerProfile> GetWorkerById(Guid id)
        {

            var worker = await _WorkerRepo.GetWorkerByIdAsync(id);

            return worker;

        }



        public async Task<string> AddAsync(WorkerProfile worker)
        {
            //check if name exist or no 
            //workerResult <=> Name

            //add
            await _WorkerRepo.AddAsync(worker);
            return "success";

        }

        public async Task<List<WorkerProfile>> GetAllWorkersAsync()
        {

            return await _WorkerRepo.GetAllWorkersAsync();

        }

        public async Task<string> DeleteWorkerAsync(Guid id)
        {

            var user = _userRepo.Find(u => u.UserId == id).FirstOrDefault();
            if (user != null) await _userRepo.DeleteAsync(user);

            var workerProfile = await _WorkerRepo.GetWorkerByIdAsync(id);
            if (workerProfile != null) await _WorkerRepo.DeleteAsync(workerProfile);


            var wallet = _walletRepo.Find(w => w.UserId == (Guid)id).FirstOrDefault();
            if (wallet != null) await _walletRepo.DeleteAsync(wallet);

            return "success";

        }

        public async Task<string> EditeWorkerAsync(WorkerProfile worker, User user)
        {
            await _WorkerRepo.UpdateAsync(worker);
            await _userRepo.UpdateAsync(user);
            return "success";
        }

        public async Task<string> ChangeWorkerPasswordAsync(WorkerProfile worker, string passward)
        {
            worker.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passward);
            await _WorkerRepo.UpdateAsync(worker);
            return "success";

        }

        public async Task<WorkerProfile> GetWorkerByWorkerId(Guid id)
        {
            return await _WorkerRepo.GetWorkerByWorkerIdAsync(id);
        }

        public async Task<string> EditeWorkerAsync(WorkerProfile worker)
        {
            await _WorkerRepo.UpdateAsync(worker);
            return "success";
        }






        #endregion



    }
}
