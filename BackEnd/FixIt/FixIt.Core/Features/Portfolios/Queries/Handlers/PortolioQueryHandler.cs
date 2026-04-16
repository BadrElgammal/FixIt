using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Portfolios.Queries.DTOs;
using FixIt.Core.Features.Portfolios.Queries.Models;
using FixIt.Core.Wrapper;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;
using System.Linq.Expressions;

namespace FixIt.Core.Features.Portfolios.Queries.Handlers
{
    public class PortolioQueryHandler : ResponseHandler,
                IRequestHandler<GetAllPortfoliosForAdminQuery, Response<List<PortfolioDTO>>>,
                IRequestHandler<GetPortoliosListByWorkerIdQuery, PaginatedResult<PortfoliosForWorkerDTO>>,
                IRequestHandler<GetAllPortfoliosByUserIdQuery, PaginatedResult<PortfolioDTO>>

    {
        private readonly IPortfolioService _portfolioService;
        private readonly IMapper _mapper;
        public PortolioQueryHandler(IPortfolioService portfolioService, IMapper mapper)
        {
            _portfolioService = portfolioService;
            _mapper = mapper;
        }


        //For Admin 
        public async Task<Response<List<PortfolioDTO>>> Handle(GetAllPortfoliosForAdminQuery request, CancellationToken cancellationToken)
        {

            var PortfoliosList = await _portfolioService.GetAllPortfoliosAsync();

            if (PortfoliosList == null || !PortfoliosList.Any()) return NotFound<List<PortfolioDTO>>("لا يوجد ");

            var PortfoliosListMapper = _mapper.Map<List<PortfolioDTO>>(PortfoliosList);


            return Success(PortfoliosListMapper);

        }


        //for another Worker
        //public async Task<Response<List<PortfolioDTO>>> Handle(GetPortoliosListByWorkerIdQuery request, CancellationToken cancellationToken)
        //{
        //    var PortfoliosList = await _portfolioService.GetAllPortfoliosByWorkerIdAsync(request.WorkerId);

        //    if (PortfoliosList == null || !PortfoliosList.Any()) return NotFound<List<PortfolioDTO>>("لا يوجد ");

        //    var PortfoliosListMapper = _mapper.Map<List<PortfolioDTO>>(PortfoliosList);

        //    return Success(PortfoliosListMapper);
        //}

        public async Task<PaginatedResult<PortfoliosForWorkerDTO>> Handle(GetPortoliosListByWorkerIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Portfolio, PortfoliosForWorkerDTO>> expression = e => new PortfoliosForWorkerDTO(e.WorkerProfile.User.FullName, e.WorkerProfile.User.ImgUrl, e.PortfolioId, e.Title, e.Description, e.ImgUrl, e.CreatedAt);
            var portfolios = _portfolioService.GetAllPortfoliosByWorkerIdpaginated(request.WorkerId);
            var portfolioPaginatedList = await portfolios.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return portfolioPaginatedList;
        }

        //for me [tocken]
        public async Task<PaginatedResult<PortfolioDTO>> Handle(GetAllPortfoliosByUserIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Portfolio, PortfolioDTO>> expression = e => new PortfolioDTO(e.PortfolioId, e.Title, e.Description, e.ImgUrl, e.CreatedAt , e.WorkerProfile.User.FullName);
            var workerId = await _portfolioService.GetWorkerIdByUserId(request.UserId);
            var portfolios = _portfolioService.GetAllPortfoliosByWorkerIdpaginated(workerId);
            var portfolioPaginatedList = await portfolios.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return portfolioPaginatedList;
        }

    }
}
