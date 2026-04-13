using FixIt.Core.Bases;
using FixIt.Core.Features.Service.Commands.DTOs;
using FixIt.Core.Features.Service.Queries.DTOs;
using FixIt.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Queries.Models
{
    public class GetRecivedServiceRequestsQuery : IRequest<PaginatedResult<GetAllServiceRequistDTO>>
    {
        public Guid Id { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        //public GetRecivedServiceRequestsQuery(Guid Id)
        //{
        //    this.Id = Id;
        //}
    }
}
