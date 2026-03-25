using FixIt.Core.Features.Portfolios.Command.Models;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Portfolios
{
    public partial class PortfolioMapper
    {

        public void EditePortfolioMapping()
        {
            CreateMap<EditePortfolioCommand, Portfolio>()
                                    .ForMember(dest => dest.ImgUrl, opt => opt.Ignore())
                                    .ForMember(dest => dest.WorkerProfileId, opt => opt.Ignore());

        }

    }
}
