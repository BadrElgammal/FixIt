using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Commands.DTOs;
using FixIt.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Queries.Models
{
    public class GetRecivedServiceRequestDetailsQuery : IRequest<Response<ServiceRequestDTO>>
    {
        public Guid ServiceId { get; set; }
        public Guid WorkerId { get; set; }
        public GetRecivedServiceRequestDetailsQuery(Guid serviceId, Guid workerId)
        {
            ServiceId = serviceId;
            WorkerId = workerId;
        }
    }
}
