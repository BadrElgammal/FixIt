using FixIt.Core.Bases;
using FixIt.Core.Features.Reports.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Reports.Queries.Models
{
    public class GetReportByReportIdQuery : IRequest<Response<ReportDTO>>
    {

        public int ReportId { get; set; }

        public GetReportByReportIdQuery(int Id)
        {
            ReportId = Id;
        }

    }
}
