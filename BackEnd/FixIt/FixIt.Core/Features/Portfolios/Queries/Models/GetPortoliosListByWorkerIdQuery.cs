using FixIt.Core.Bases;
using FixIt.Core.Features.Portfolios.Queries.DTOs;
using FixIt.Core.Wrapper;
using MediatR;

namespace FixIt.Core.Features.Portfolios.Queries.Models
{
    public class GetPortoliosListByWorkerIdQuery : IRequest<PaginatedResult<PortfoliosForWorkerDTO>>
    {
        public Guid WorkerId { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }
    }
}
