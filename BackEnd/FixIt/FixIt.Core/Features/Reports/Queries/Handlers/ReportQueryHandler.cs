using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Reports.Queries.DTOs;
using FixIt.Core.Features.Reports.Queries.Models;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Reports.Queries.Handlers
{
    public class ReportQueryHandler : ResponseHandler,
                IRequestHandler<GetAllReportsQuery, Response<List<ReportDTO>>>,
                IRequestHandler<GetReportByReportIdQuery, Response<ReportDTO>>
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportQueryHandler(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }


        public async Task<Response<List<ReportDTO>>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {

            var ReportsList = await _reportService.GetAllReportsAsync();
            if (ReportsList == null || !ReportsList.Any()) return NotFound<List<ReportDTO>>("لا يوجد ");

            var MappedList = _mapper.Map<List<ReportDTO>>(ReportsList);

            return Success(MappedList);
        }

        public async Task<Response<ReportDTO>> Handle(GetReportByReportIdQuery request, CancellationToken cancellationToken)
        {
            var report = await _reportService.GetReportByReportIdAsync(request.ReportId);
            if (report == null) return NotFound<ReportDTO>("غير موجود");

            var MappedReport = _mapper.Map<ReportDTO>(report);

            return Success(MappedReport);
        }



    }
}
