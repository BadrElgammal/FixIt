using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Commands.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Queries.Models
{
    public class GetSentsServiceRequestDetailsQuery : IRequest<Response<ServiceRequestDTO>>
    {
        public Guid ServiceId { get; set; }
        public Guid ClientId { get; set; }
        public GetSentsServiceRequestDetailsQuery(Guid serviceId, Guid clinetId)
        {
            ServiceId = serviceId;
            ClientId = clinetId;
        }
    }
}
