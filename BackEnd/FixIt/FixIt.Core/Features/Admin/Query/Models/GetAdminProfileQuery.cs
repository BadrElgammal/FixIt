using FixIt.Core.Bases;
using FixIt.Core.Features.Admin.Query.DTOs;
using MediatR;

namespace FixIt.Core.Features.Admin.Query.Models
{
    public class GetAdminProfileQuery : IRequest<Response<AdminProfileDTO>>
    {
        public Guid UserId { get; set; }

        public GetAdminProfileQuery(Guid Id)
        {
            UserId = Id;
        }


    }
}
