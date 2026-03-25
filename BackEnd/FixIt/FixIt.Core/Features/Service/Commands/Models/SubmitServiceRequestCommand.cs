using FixIt.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class SubmitServiceRequestCommand : IRequest<Response<string>>
    {
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }

        public SubmitServiceRequestCommand()
        {

        }


        public SubmitServiceRequestCommand(Guid serviceId, Guid userId)
        {
            ServiceId = serviceId;
            UserId = userId;
        }

        //Img
        public IFormFile? SubmitedImgUrl { get; set; }

    }
}
