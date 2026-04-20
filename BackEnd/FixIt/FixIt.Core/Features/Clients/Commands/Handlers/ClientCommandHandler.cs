using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Clients.Commands.Models;
using FixIt.Domain.Entities;
using FixIt.Domain.Enum;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Clients.Commands.Handlers
{
    public class ClientCommandHandler : ResponseHandler, IRequestHandler<EditClientCommand, Response<String>>
        , IRequestHandler<DeleteClientCommand, Response<String>>
        , IRequestHandler<ChangeClientPasswordCommand, Response<String>>
        , IRequestHandler<ChangeClientImageURL, Response<String>>
        , IRequestHandler<AddClientByAdminCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;
        private readonly IService<User> _UserService;
        private readonly IService<Wallet> _WalletService;


        public ClientCommandHandler(IMapper mapper, IClientService clientService,
            IService<User> UserService, IService<Wallet> WalletService)
        {
            _mapper = mapper;
            _clientService = clientService;
            _UserService = UserService;
            _WalletService = WalletService;
        }
        public async Task<Response<string>> Handle(EditClientCommand request, CancellationToken cancellationToken)
        {
            var Client = await _clientService.GetClientById(request.UserId);
            if (Client == null) return NotFound<String>("المستخدم غير موجود");

            _mapper.Map(request, Client);

            var result = await _clientService.EditClinetAsync(Client);

            if (result == "success") return Success("تم التعديل بنجاح");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var Client = await _clientService.GetClientById(request.Id);
            if (Client == null) return NotFound<String>("المستخدم غير موجود");
            var result = await _clientService.DeleteClientAsync(Client);

            if (result == "success") return Deleted<String>("تم الحذف بنجاح");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(ChangeClientPasswordCommand request, CancellationToken cancellationToken)
        {
            var Client = await _clientService.GetClientById(request.UserId);
            if (Client == null) return NotFound<String>("المستخدم غير موجود");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, Client.PasswordHash);
            if (!checkPassword) return BadRequest<string>("كلمة المرور الحاليه غير صحيحه");

            var result = await _clientService.ChangeClinetPasswordAsync(Client, request.NewPassword);
            if (result == "success") return Success("تم التعديل الباسورد بنجاح");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(ChangeClientImageURL request, CancellationToken cancellationToken)
        {
            var user = await _clientService.GetClientById(request.UserId);

            var result = await _clientService.ChangeClientImage(user, request.ImgUrl);


            switch (result)
            {
                case "No Image !!": return NotFound<string>("No Image !!");
                case "Feild to Uplaod !!": return NotFound<string>("Feild to Uplaod !!");
                case "FaildinAdd": return NotFound<string>("FaildinAdd");

            }

            return Success("تم تغير الصورة ");


        }


        //Add Client by Admin
        public async Task<Response<string>> Handle(AddClientByAdminCommand request, CancellationToken cancellationToken)
        {

            if (_UserService.Find(u => u.Email == request.Email).Any())
                return BadRequest<string>("هذا البريد الإلكتروني مستخدم بالفعل. يرجى استخدام بريد آخر.");


            if (_UserService.Find(u => u.Phone == request.Phone).Any())
                return BadRequest<string>("رقم الهاتف مستخدم بالفعل. يرجى استخدام رقم اخر.");


            if (request.Password != request.ConfirmPassword)
                return BadRequest<string>("كلمة المرور وتأكيد كلمة المرور غير متطابقتين.");

            try
            {

                var user = new User
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    Phone = request.Phone,
                    City = request.City,
                    Role = RoleType.client.ToString(),//"client"
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };

                await _UserService.AddAsync(user);


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
    }
}
