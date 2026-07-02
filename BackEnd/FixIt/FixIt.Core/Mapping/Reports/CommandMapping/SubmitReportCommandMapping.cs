using FixIt.Core.Features.Reports.Command.Models;
using FixIt.Domain.Entities;
using FixIt.Domain.Enum;

namespace FixIt.Core.Mapping.Reports
{
    public partial class ReportMapper
    {


        public void SubmitReportCommandMapping()
        {
            CreateMap<SubmitReportCommand, Report>()
                .ForMember(dest => dest.ReportType, opt => opt.MapFrom(src =>
                    Enum.IsDefined(typeof(ReportCategory), src.ReportType)
                        ? (ReportCategory)Enum.Parse(typeof(ReportCategory), src.ReportType, true)
                        : ReportCategory.Other
                ));
        }
    }
}
