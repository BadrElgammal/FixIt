using FixIt.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FixIt.Core.Features.Admin.Command.Models
{
    public class ChangeAdminImgURLCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }

        public ChangeAdminImgURLCommand() { }

        public ChangeAdminImgURLCommand(Guid id)
        {
            UserId = id;
        }

        public IFormFile? ImgUrl { get; set; }

    }
}
