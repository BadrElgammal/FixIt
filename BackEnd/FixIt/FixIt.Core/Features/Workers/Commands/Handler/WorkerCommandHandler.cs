using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Commands.Models;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Workers.Commands.Handler
{
    public class WorkerCommandHandler : ResponseHandler
                    , IRequestHandler<EditeWorkerCommand, Response<string>>,
                     IRequestHandler<DeleteWorkerCommand, Response<string>>,
                     IRequestHandler<ChangeWorkerPasswordCommand, Response<string>>
    {

        #region Feilds
        private readonly IWorkerService _workerService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctors
        public WorkerCommandHandler(IWorkerService workerService, IMapper mapper)
        {
            _workerService = workerService;
            _mapper = mapper;
        }
        #endregion


        #region Methods Handel

        public async Task<Response<string>> Handle(EditeWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await _workerService.GetWorkerById(request.UserId);
            if (worker == null) return NotFound<String>("المستخدم غير موجود");


            //worProfile    =>// user
            var workerMapper = _mapper.Map<WorkerProfile>(request);
            var UserMapper = _mapper.Map<User>(request);

            var result = await _workerService.EditeWorkerAsync(workerMapper, UserMapper);

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

        #endregion
    }
}
