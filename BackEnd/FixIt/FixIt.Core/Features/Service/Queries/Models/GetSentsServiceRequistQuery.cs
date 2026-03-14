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
    public class GetSentsServiceRequistQuery : IRequest<Response<List<ServiceRequestDTO>>>
    {
        public Guid Id { get; set; }
        public GetSentsServiceRequistQuery(Guid Id)
        {
            this.Id = Id;
        }
    }
}
