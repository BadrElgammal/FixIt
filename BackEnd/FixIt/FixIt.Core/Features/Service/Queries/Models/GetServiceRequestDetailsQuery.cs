using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Queries.Models
{
    public class GetServiceRequestDetailsQuery : IRequest<Response<ServiceRequestDetailsDTO>>
    {
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public GetServiceRequestDetailsQuery(Guid serviceId, Guid userId , string role )
        {
            ServiceId = serviceId;
            UserId = userId;
            Role = role;
        }
    }
}
