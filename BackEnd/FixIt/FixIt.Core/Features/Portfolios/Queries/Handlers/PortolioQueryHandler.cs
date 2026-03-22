using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Portfolios.Queries.DTOs;
using FixIt.Core.Features.Portfolios.Queries.Models;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Portfolios.Queries.Handlers
{
    public class PortolioQueryHandler : ResponseHandler,
                IRequestHandler<GetPortoliosListByWorkerIdQuery, Response<List<PortfolioDTO>>>,
                IRequestHandler<GetAllPortfoliosByUserIdQuery, Response<List<PortfolioDTO>>>

    {
        private readonly IPortfolioService _portfolioService;
        private readonly IMapper _mapper;
        public PortolioQueryHandler(IPortfolioService portfolioService, IMapper mapper)
        {
            _portfolioService = portfolioService;
            _mapper = mapper;
        }

        public async Task<Response<List<PortfolioDTO>>> Handle(GetPortoliosListByWorkerIdQuery request, CancellationToken cancellationToken)
        {
            var PortfoliosList = await _portfolioService.GetAllPortfoliosByWorkerIdAsync(request.WorkerId);
            var PortfoliosListMapper = _mapper.Map<List<PortfolioDTO>>(PortfoliosList);


            return Success(PortfoliosListMapper);
        }

        public async Task<Response<List<PortfolioDTO>>> Handle(GetAllPortfoliosByUserIdQuery request, CancellationToken cancellationToken)
        {
            var PortfoliosList = await _portfolioService.GetAllPortfoliosByWorkerIdAsync(request.UserId);
            var PortfoliosListMapper = _mapper.Map<List<PortfolioDTO>>(PortfoliosList);


            return Success(PortfoliosListMapper);
        }
    }
}
