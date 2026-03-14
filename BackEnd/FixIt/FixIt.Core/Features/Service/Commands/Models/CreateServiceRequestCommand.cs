using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Commands.Models
{
    public class CreateServiceRequestCommand : IRequest<Response<String>>
    {
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }
        [JsonIgnore]
        public Guid ClientId { get; set; }
        public Guid WorkerId { get; set; }
    }
}
