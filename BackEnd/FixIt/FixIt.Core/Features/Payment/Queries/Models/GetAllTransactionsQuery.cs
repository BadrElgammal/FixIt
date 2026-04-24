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
    public class GetAllTransactionsQuery : IRequest<PaginatedResult<GetAllTransactionsQueryDTO>>
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public GetAllTransactionsQuery(int pageNum, int pageSize)
        {
            PageNum = pageNum;
            PageSize = pageSize;
        }
    }
}
