using FixIt.Core.Bases;
using FixIt.Core.Features.Portfolios.Queries.DTOs;
using FixIt.Core.Wrapper;
using MediatR;

namespace FixIt.Core.Features.Portfolios.Queries.Models
{
    public class GetAllPortfoliosByUserIdQuery : IRequest<PaginatedResult<PortfolioDTO>>
    {
        public Guid UserId { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }

    }
}
