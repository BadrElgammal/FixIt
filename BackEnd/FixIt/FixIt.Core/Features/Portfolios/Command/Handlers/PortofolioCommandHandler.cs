using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Portfolios.Command.Models;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Portfolios.Command.Handlers
{
    public class PortofolioCommandHandler : ResponseHandler,
               IRequestHandler<AddPortfolioCommand, Response<string>>,
               IRequestHandler<DeletePortfolioCommand, Response<string>>,
               IRequestHandler<EditePortfolioCommand, Response<string>>
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IMapper _mapper;
        public PortofolioCommandHandler(IPortfolioService portfolioService, IMapper mapper)
        {
            _portfolioService = portfolioService;
            _mapper = mapper;

        }

        public async Task<Response<string>> Handle(AddPortfolioCommand request, CancellationToken cancellationToken)
        {

            var portfolioMapper = _mapper.Map<Portfolio>(request);
            portfolioMapper.WorkerProfileId = await _portfolioService.GetWorkerIdByUserId(request.WorkerProfileId);
            var result = await _portfolioService.AddPortfolioAsync(portfolioMapper, request.ImgUrl);

            switch (result)
            {
                case "No Image !!": return NotFound<string>("No Image !!");
                case "Feild to Uplaod !!": return NotFound<string>("Feild to Uplaod !!");
                case "FaildinAdd": return NotFound<string>("FaildinAdd");

                case "success": return Success($"تم اضافة  {request.Title}");
                default: return NotFound<string>($"{result}");
            }


            return BadRequest<string>();


        }

        public async Task<Response<string>> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
        {


            var portfolio = await _portfolioService.GetPortfolioByidAsync(request.PortfolioId);
            if (portfolio == null) return NotFound<string>("غير موجود");


            var result = await _portfolioService.DeletePortfolioAsync(portfolio);
            if (result == "success") return Success("تم الحذف ");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditePortfolioCommand request, CancellationToken cancellationToken)
        {

            var portfolio = await _portfolioService.GetPortfolioByidAsync(request.PortfolioId);
            if (portfolio == null) return NotFound<string>("غير موجود");


            var portfolioMapper = _mapper.Map<Portfolio>(request);

            portfolioMapper.WorkerProfileId = await _portfolioService.GetWorkerIdByUserId(request.WorkerProfileId);

            var result = await _portfolioService.UpdatePortfolioAsync(portfolioMapper, request.ImgUrl);

            switch (result)
            {
                case "No Image !!": return NotFound<string>("No Image !!");
                case "Feild to Uplaod !!": return NotFound<string>("Feild to Uplaod !!");
                case "FaildinAdd": return NotFound<string>("FaildinAdd");

                case "success": return Success("تم التعديل ");
            }

            return BadRequest<string>();

        }


    }
}
