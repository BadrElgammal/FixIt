using FixIt.Core.Bases;
using FixIt.Core.Features.Reports.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Reports.Queries.Models
{
    public class GetAllReportsQuery : IRequest<Response<List<ReportDTO>>>
    {


    }
}
