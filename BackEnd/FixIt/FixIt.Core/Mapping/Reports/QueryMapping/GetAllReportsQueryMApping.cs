using FixIt.Core.Features.Reports.Queries.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Reports
{
    public partial class ReportMapper
    {
        public void GetAllReportsQueryMApping()
        {
            CreateMap<Report, ReportDTO>()
                       .ForMember(dest => dest.ReportedUserName, opt => opt.MapFrom(src => src.ReportedUser.FullName))
                       .ForMember(dest => dest.ReporterUserName, opt => opt.MapFrom(src => src.ReporterUser.FullName));




        }

    }
}
