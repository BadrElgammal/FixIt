using FixIt.Core.Features.Service.Queries.DTOs;
using FixIt.Core.Wrapper;
using FixIt.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Service.Queries.Models
{
    public class GetAllServiceRequestsQuery :IRequest<PaginatedResult<GetAllServiceRequistDTO>>
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public ServiceRequestState? state { get; set; }
    }
}
