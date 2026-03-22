using FixIt.Core.Bases;
using FixIt.Core.Features.Portfolios.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Portfolios.Queries.Models
{
    public class GetPortoliosListByWorkerIdQuery : IRequest<Response<List<PortfolioDTO>>>
    {
        public Guid WorkerId { get; set; }
        public GetPortoliosListByWorkerIdQuery(Guid id)
        {
            this.WorkerId = id;
        }
    }
}
