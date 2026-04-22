using FixIt.Core.Features.Reports.Command.Models;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Reports
{
    public partial class ReportMapper
    {


        public void SubmitReportCommandMapping()
        {
            CreateMap<SubmitReportCommand, Report>();

        }

    }
}
