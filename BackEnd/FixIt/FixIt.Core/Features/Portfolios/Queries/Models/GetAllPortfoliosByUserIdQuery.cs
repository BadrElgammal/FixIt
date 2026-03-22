using FixIt.Core.Bases;
using FixIt.Core.Features.Portfolios.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Portfolios.Queries.Models
{
    public class GetAllPortfoliosByUserIdQuery : IRequest<Response<List<PortfolioDTO>>>
    {
        public Guid UserId { get; set; }
        public GetAllPortfoliosByUserIdQuery(Guid id)
        {
            UserId = id;
        }


    }
}
