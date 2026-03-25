using FixIt.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class CreateServiceRequestCommand : IRequest<Response<String>>
    {
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        [JsonIgnore]
        public Guid ClientId { get; set; }
        public Guid WorkerId { get; set; }

        //Img
        public IFormFile? RequestedImgUrl { get; set; }

    }
}
