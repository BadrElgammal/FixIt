using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Service.Services
{
    public class WorkerService : IWorkerService
    {

        #region Fields
        private readonly IWorkerRepository _WorkerRepo;
        private readonly IGenericRepositoryAsync<Wallet> _walletRepo;
        private readonly IGenericRepositoryAsync<User> _userRepo;
        private readonly IFileService _fileService;


        #endregion

        #region Ctors

        public WorkerService(IWorkerRepository WorkerRepo, IGenericRepositoryAsync<Wallet> walletRepo
            , IGenericRepositoryAsync<User> userRepo, IFileService fileService)
        {
            this._WorkerRepo = WorkerRepo;
            _walletRepo = walletRepo;
            _userRepo = userRepo;
            _fileService = fileService;
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

        public async Task<string> ChangeWorkerImage(User user, IFormFile file)
        {


            var ImgUrl = await _fileService.UploadImage("Workers", file);
            user.ImgUrl = ImgUrl;

            switch (ImgUrl)
            {
                case "No Image !!": return "No Image !!";
                case "Feild to Uplaod !!": return "Feild to Uplaod !!";
            }

            try
            {
                await _userRepo.UpdateAsync(user);
                return "success";

            }
            catch (Exception)
            {

                return "FaildinAdd";
            }


        }

        public async Task<User> GetUserByUserId(Guid userId)
        {
            return await _userRepo.GetByIdAsync(userId);
        }

        public IQueryable<WorkerProfile> GetAllWorkersPaginated()
        {
            return _WorkerRepo.GetTableNoTracking().Include(w => w.User)
                        .Include(w => w.Category).AsQueryable();
        }

        public IQueryable<WorkerProfile> GetAllWorkersPaginatedWithFiltaration(string search, string address, bool? isAvilable)
        {
            var query = _WorkerRepo.GetTableNoTracking().Include(w => w.User)
                        .Include(w => w.Category).AsQueryable();
            if (search != null) query = query.Where(w => w.User.FullName.Contains(search));
            if (address != null) query = query.Where(w => w.Area.Contains(address) || w.User.City.Contains(address));
            if (isAvilable != null) query = query.Where(w => w.AvailabilityStatus == isAvilable);
            return query;
        }






        #endregion



    }
}
