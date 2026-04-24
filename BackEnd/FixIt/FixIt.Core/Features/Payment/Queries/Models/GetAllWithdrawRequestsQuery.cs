using FixIt.Core.Features.Payment.Queries.DTOs;
using FixIt.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Payment.Queries.Models
{
    public class GetAllWithdrawRequestsQuery : IRequest<PaginatedResult<WithdrawRequestsQueryDTO>>
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public string? status { get; set; }
    }
}
