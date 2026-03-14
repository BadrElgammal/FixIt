using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Commands.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class RejectServiceRequestCommand : IRequest<Response<String>>
    {
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
        public RejectServiceRequestCommand(Guid serviceId, Guid userId)
        {
            ServiceId = serviceId;
            UserId = userId;
        }
    }
}
