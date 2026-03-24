using FixIt.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FixIt.Core.Features.Workers.Commands.Models
{
    public class ChangeWorkerImgURL : IRequest<Response<string>>
    {
        public Guid userId { get; set; }
        public ChangeWorkerImgURL(Guid id)
        {
            userId = id;
        }

        public IFormFile? ImgUrl { get; set; }

    }
}
