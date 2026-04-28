using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Commands.Models;
using FixIt.Domain.Entities;
using FixIt.Domain.Enum;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Workers.Commands.Handler
{
    public class WorkerCommandHandler : ResponseHandler
                    , IRequestHandler<EditeWorkerCommand, Response<string>>,
                     IRequestHandler<DeleteWorkerCommand, Response<string>>,
                     IRequestHandler<ChangeWorkerPasswordCommand, Response<string>>,
                     IRequestHandler<ChangeWorkerImgURL, Response<string>>,
                     IRequestHandler<AddWorkerByAdminCommand, Response<string>>
    {

        #region Feilds
        private readonly IWorkerService _workerService;
        private readonly ICategoryService _categoryService;
        private readonly IWorkerRepository _workerRepo;
        private readonly IService<User> _UserService;
        private readonly IService<WorkerProfile> _WorkerProfileService;
        private readonly IService<Wallet> _WalletService;
        private readonly IMapper _mapper;

        #endregion

        #region Ctors
        public WorkerCommandHandler(IWorkerService workerService, IMapper mapper, ICategoryService categoryService
            , IWorkerRepository workerRepo, IService<User> UserService, IService<Wallet> walletService, IService<WorkerProfile> WorkerProfileService)
        {
            _workerService = workerService;
            _mapper = mapper;
            _categoryService = categoryService;
            _workerRepo = workerRepo;
            _WalletService = walletService;
            _UserService = UserService;
            _WorkerProfileService = WorkerProfileService;
        }
        #endregion


        #region Methods Handel

        public async Task<Response<string>> Handle(EditeWorkerCommand request, CancellationToken cancellationToken)
        {

            var WorkerId = await _workerRepo.GetWorkerIdByUserIdAsync(request.UserId);

            var worker = await _workerService.GetWorkerByWorkerId(WorkerId);
            if (worker == null) return NotFound<string>("المستخدم غير موجود");


            var category = await _categoryService.GetCategoryByNameAsync(request.CategoryName);
            if (category == null) return NotFound<string>("هذا القسم غير موجود");
            worker.CategoryId = category.CategoryId;



            var workerMapper = _mapper.Map(request, worker);

            var result = await _workerService.EditeWorkerAsync(workerMapper);


            if (result == "success") return Success("تم التعديل بنجاح");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await _workerService.GetWorkerById(request.Id);
            if (worker == null) return NotFound<String>("المستخدم غير موجود");


            var result = await _workerService.DeleteWorkerAsync(worker.UserId);

            if (result == "success") return Success("تم الحذف بنجاح");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(ChangeWorkerPasswordCommand request, CancellationToken cancellationToken)
        {
            var worker = await _workerService.GetWorkerById(request.UserId);
            if (worker == null) return NotFound<String>("المستخدم غير موجود");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, worker.User.PasswordHash);
            if (!checkPassword) return BadRequest<string>("كلمة المرور الحاليه غير صحيحه");

            var result = await _workerService.ChangeWorkerPasswordAsync(worker, request.NewPassword);
            if (result == "success") return Success("تم التعديل الباسورد بنجاح");
            else return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(ChangeWorkerImgURL request, CancellationToken cancellationToken)
        {

            var user = await _workerService.GetUserByUserId(request.userId);

            var result = await _workerService.ChangeWorkerImage(user, request.ImgUrl);


            switch (result)
            {
                case "No Image !!": return NotFound<string>("No Image !!");
                case "Feild to Uplaod !!": return NotFound<string>("Feild to Uplaod !!");
                case "FaildinAdd": return NotFound<string>("FaildinAdd");

            }

            return Success("تم تغير الصورة ");

        }


        //Add worker by Admin
        public async Task<Response<string>> Handle(AddWorkerByAdminCommand request, CancellationToken cancellationToken)
        {


            if (_UserService.Find(u => u.Email == request.Email).Any())
                return BadRequest<string>("هذا البريد الإلكتروني مستخدم بالفعل. يرجى استخدام بريد آخر.");


            if (_UserService.Find(u => u.Phone == request.Phone).Any())
                return BadRequest<string>("رقم الهاتف مستخدم بالفعل. يرجى استخدام رقم اخر.");


            if (request.Password != request.ConfirmPassword)
                return BadRequest<string>("كلمة المرور وتأكيد كلمة المرور غير متطابقتين.");



            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                City = request.City,
                Role = RoleType.worker.ToString(), //"worker"
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            try
            {

                await _UserService.AddAsync(user);

                var worker = new WorkerProfile() { UserId = user.UserId };
                await _WorkerProfileService.AddAsync(worker);

                var wallet = new Wallet()
                {
                    UserId = user.UserId,
                    OwnerType = user.Role
                };
                await _WalletService.AddAsync(wallet);
            }
            catch (Exception)
            {

                return BadRequest<string>("فشلت عمليةالاضافة ");

            }

            return Success("تم الاضافة بنجاح");

        }


        #endregion
    }
}
