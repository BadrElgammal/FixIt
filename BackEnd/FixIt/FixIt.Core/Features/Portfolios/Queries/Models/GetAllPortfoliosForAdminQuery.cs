using FixIt.Core.Bases;
using FixIt.Core.Features.Portfolios.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Portfolios.Queries.Models
{
    public class GetAllPortfoliosForAdminQuery : IRequest<Response<List<PortfolioDTO>>>
    {


    }
}
