using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class AcceptPriceServiceRequestCommand :IRequest<Response<string>>
    {
        public Guid ServiceId { get; set; }
        public Guid ClientId { get; set; }
        public AcceptPriceServiceRequestCommand(Guid serviceId, Guid clientId)
        {
            ServiceId = serviceId;
            ClientId = clientId;
        }
    }
}
