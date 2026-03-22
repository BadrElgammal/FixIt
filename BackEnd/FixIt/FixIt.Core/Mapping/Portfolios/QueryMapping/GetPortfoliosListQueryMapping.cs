using FixIt.Core.Features.Portfolios.Queries.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Portfolios
{
    public partial class PortfolioMapper
    {
        public void GetPortfoliosListQueryMapping()
        {

            CreateMap<Portfolio, PortfolioDTO>()
                     .ForMember(dest => dest.WorkerFullName, opt => opt.MapFrom(src => src.WorkerProfile.User.FullName));

        }

    }
}
