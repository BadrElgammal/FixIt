using FixIt.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FixIt.Core.Features.Clients.Commands.Models
{
    public class ChangeClientImageURL : IRequest<Response<String>>
    {
        public Guid UserId { get; set; }
        public ChangeClientImageURL(Guid id)
        {
            UserId = id;
        }

        public IFormFile? ImgUrl { get; set; }

    }
}
