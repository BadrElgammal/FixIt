using AutoMapper;

namespace FixIt.Core.Mapping.Reports
{
    public partial class ReportMapper : Profile
    {
        public ReportMapper()
        {
            SubmitReportCommandMapping();
            GetAllReportsQueryMApping();

        }

    }
}
