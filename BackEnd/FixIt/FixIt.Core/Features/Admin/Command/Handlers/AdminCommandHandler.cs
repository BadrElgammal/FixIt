using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Admin.Command.Models;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Admin.Command.Handlers
{
    public class AdminCommandHandler : ResponseHandler,
        IRequestHandler<EditeAdminCommand, Response<string>>,
        IRequestHandler<DeleteAdminCommand, Response<string>>,
        IRequestHandler<ChangeAdminPasswordCommand, Response<string>>,
        IRequestHandler<ChangeAdminImgURLCommand, Response<string>>

    {

        private readonly IAdminService _AdminService;
        private readonly IMapper _mapper;


        public AdminCommandHandler(IAdminService AdminService, IMapper mapper)
        {
            _AdminService = AdminService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(EditeAdminCommand request, CancellationToken cancellationToken)
        {

            //get admin/check if exist
            var admin = await _AdminService.GetByIdAsync(request.UserId);
            if (admin == null) return NotFound<string>("المستخدم غير موجود");

            //mapp
            var mappedAdmin = _mapper.Map(request, admin);

            //edite 
            var result = await _AdminService.EditeAdminAsync(mappedAdmin);

            if (result == "success") return Success("تم التعديل بنجاح");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            //get check
            var admin = await _AdminService.GetByIdAsync(request.userId);
            if (admin == null) return NotFound<string>("المستخدم غير موجود");

            //delete
            var result = await _AdminService.DeleteAdminAsync(admin);

            //success
            if (result == "success") return Success("تم الحذف بنجاح");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(ChangeAdminPasswordCommand request, CancellationToken cancellationToken)
        {
            //get check
            var admin = await _AdminService.GetByIdAsync(request.UserId);
            if (admin == null) return NotFound<string>("المستخدم غير موجود");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, admin.PasswordHash);
            if (!checkPassword) return BadRequest<string>("كلمة المرور الحاليه غير صحيحه");

            //change
            var result = await _AdminService.ChangeAdminPasswordAsync(admin, request.NewPassword);

            //success
            if (result == "success") return Success("تم تغيير الياسورد بنجاح");
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(ChangeAdminImgURLCommand request, CancellationToken cancellationToken)
        {
            var admin = await _AdminService.GetByIdAsync(request.UserId);

            var result = await _AdminService.ChangeClientImage(admin, request.ImgUrl);


            switch (result)
            {
                case "No Image !!": return NotFound<string>("No Image !!");
                case "Feild to Uplaod !!": return NotFound<string>("Feild to Uplaod !!");
                case "FaildinAdd": return NotFound<string>("FaildinAdd");

            }

            return Success("تم تغير الصورة بنجاح");

        }
    }
}
