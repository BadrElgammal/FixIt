using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Commands.DTOs;
using FixIt.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class AddPriceToServiceRequestCommand : IRequest<Response<string>>
    {
        [JsonIgnore]
        public Guid RequestId { get; set; } 
        public decimal TotalPrice { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
