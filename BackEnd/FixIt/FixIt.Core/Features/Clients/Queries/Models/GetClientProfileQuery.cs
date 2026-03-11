using FixIt.Core.Features.Clients.Queries.DTOs;
using FixIt.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Clients.Queries.Models
{
    public class GetClientProfileQuery : IRequest<Response<ClientProfileDTO>>
    {
        public Guid Id { get; set; }
        public GetClientProfileQuery(Guid Id)
        {
            this.Id = Id;
        }
    }
}
