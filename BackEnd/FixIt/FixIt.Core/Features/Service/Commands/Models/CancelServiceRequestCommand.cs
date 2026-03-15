using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class CancelServiceRequestCommand :IRequest<Response<string>>
    {
        public Guid ServiceId { get; set; }
        public Guid WorkerId { get; set; }
        public CancelServiceRequestCommand(Guid serviceId, Guid workerId)
        {
            ServiceId = serviceId;
            WorkerId = workerId;
        }
    }
}
