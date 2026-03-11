using MediatR;
using FixIt.Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FixIt.Domain.Entities;

namespace FixIt.Core.Features.Clients.Queries.Models
{
    public class GetClientsListQuery : IRequest<Response<List<User>>>
    {
    }
}
