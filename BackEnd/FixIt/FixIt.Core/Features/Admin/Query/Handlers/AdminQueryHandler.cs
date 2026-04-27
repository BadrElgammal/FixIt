using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Admin.Query.DTOs;
using FixIt.Core.Features.Admin.Query.Models;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Admin.Query.Handlers
{
    public class AdminQueryHandler : ResponseHandler,
        IRequestHandler<GetAdminProfileQuery, Response<AdminProfileDTO>>


    {

        private readonly IAdminService _AdminService;
        private readonly IMapper _mapper;

        public AdminQueryHandler(IAdminService AdminService, IMapper mapper)
        {
            _AdminService = AdminService;
            _mapper = mapper;

        }

        public async Task<Response<AdminProfileDTO>> Handle(GetAdminProfileQuery request, CancellationToken cancellationToken)
        {
            var admin = await _AdminService.GetByIdAsync(request.UserId);
            if (admin == null) return NotFound<AdminProfileDTO>("المستخدم غير موجود|غير صحيح");
            //mapp
            var mappedAdmin = _mapper.Map<AdminProfileDTO>(admin);
            return Success(mappedAdmin);
        }


    }
}
