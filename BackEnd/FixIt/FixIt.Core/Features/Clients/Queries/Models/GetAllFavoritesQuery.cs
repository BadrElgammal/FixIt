using FixIt.Core.Bases;
using FixIt.Core.Features.Clients.Queries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Clients.Queries.Models
{
    public class GetAllFavoritesQuery:IRequest<Response<List<ClientFavoritesWorkerDTO>>>
    {
        public Guid Id { get; set; }
        public GetAllFavoritesQuery(Guid Id)
        {
            this.Id = Id;
        }
    }
}
