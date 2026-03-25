using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class AcceptPriceServiceRequestCommand :IRequest<Response<string>>
    {
        public Guid ServiceId { get; set; }
        [JsonIgnore]
        public Guid ClientId { get; set; }
        public string? serviceAddress { get; set; }
    }
}
