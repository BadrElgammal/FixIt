using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Portfolios.Queries.DTOs;
using FixIt.Core.Features.Portfolios.Queries.Models;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Portfolios.Queries.Handlers
{
    public class PortolioQueryHandler : ResponseHandler,
                IRequestHandler<GetAllPortfoliosForAdminQuery, Response<List<PortfolioDTO>>>,
                IRequestHandler<GetPortoliosListByWorkerIdQuery, Response<PortfoliosForWorkerDTO>>,
                IRequestHandler<GetAllPortfoliosByUserIdQuery, Response<List<PortfolioDTO>>>

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

        async Task<Response<PortfoliosForWorkerDTO>> IRequestHandler<GetPortoliosListByWorkerIdQuery, Response<PortfoliosForWorkerDTO>>.Handle(GetPortoliosListByWorkerIdQuery request, CancellationToken cancellationToken)
        {
            var worker = await _portfolioService.GetWorkerByWorkerId(request.WorkerId);

            if (worker == null) return NotFound<PortfoliosForWorkerDTO>("هذا العامل غير موجود");
            if (worker.Portfolios == null || !worker.Portfolios.Any()) return NotFound<PortfoliosForWorkerDTO>("لا يوجد اعمال ");

            var PortfoliosListMapper = _mapper.Map<PortfoliosForWorkerDTO>(worker);

            return Success(PortfoliosListMapper);


        }


        //for me [tocken]
        public async Task<Response<List<PortfolioDTO>>> Handle(GetAllPortfoliosByUserIdQuery request, CancellationToken cancellationToken)
        {

            var workerId = await _portfolioService.GetWorkerIdByUserId(request.UserId);
            var PortfoliosList = await _portfolioService.GetAllPortfoliosByWorkerIdAsync(workerId);

            if (PortfoliosList == null || !PortfoliosList.Any()) return NotFound<List<PortfolioDTO>>("لا يوجد ");

            var PortfoliosListMapper = _mapper.Map<List<PortfolioDTO>>(PortfoliosList);


            return Success(PortfoliosListMapper);
        }


    }
}
