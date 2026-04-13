using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class AcceptSubmittedServiceRequestCommand : IRequest<Response<string>>
    {
        public Guid ServiceId { get; set; }
        public Guid ClientId { get; set; }
        public string Rule { get; set; }
        public AcceptSubmittedServiceRequestCommand(Guid serviceId, Guid clientId , string rule)
        {
            ServiceId = serviceId;
            ClientId = clientId;
            Rule = rule;
        }
    }
}
