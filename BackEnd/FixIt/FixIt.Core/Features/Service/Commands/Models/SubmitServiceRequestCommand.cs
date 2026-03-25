using FixIt.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class SubmitServiceRequestCommand : IRequest<Response<string>>
    {
        public Guid ServiceId { get; set; }
        public Guid WorkerId { get; set; }

        public SubmitServiceRequestCommand()
        {

        }


        public SubmitServiceRequestCommand(Guid serviceId, Guid workerId)
        {
            ServiceId = serviceId;
            WorkerId = workerId;
        }

        //Img
        public IFormFile? SubmitedImgUrl { get; set; }

    }
}
