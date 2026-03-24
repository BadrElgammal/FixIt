using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Commands.Models;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Workers.Commands.Handler
{
    public class WorkerCommandHandler : ResponseHandler
                    , IRequestHandler<EditeWorkerCommand, Response<string>>,
                     IRequestHandler<DeleteWorkerCommand, Response<string>>,
                     IRequestHandler<ChangeWorkerPasswordCommand, Response<string>>,
                     IRequestHandler<ChangeWorkerImgURL, Response<string>>
    {

        #region Feilds
        private readonly IWorkerService _workerService;
        private readonly ICategoryService _categoryService;
        private readonly IWorkerRepository _workerRepo;
        private readonly IClientService _userService;
        private readonly IMapper _mapper;

        #endregion

        #region Ctors
        public WorkerCommandHandler(IWorkerService workerService, IMapper mapper, ICategoryService categoryService
            , IWorkerRepository workerRepo)
        {
            _workerService = workerService;
            _mapper = mapper;
            _categoryService = categoryService;
            _workerRepo = workerRepo;

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

        #endregion
    }
}
