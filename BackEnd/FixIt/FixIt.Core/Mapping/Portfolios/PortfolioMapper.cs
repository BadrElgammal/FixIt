using AutoMapper;

namespace FixIt.Core.Mapping.Portfolios
{
    public partial class PortfolioMapper : Profile
    {
        public PortfolioMapper()
        {
            AddPortfolioMapping();
            GetPortfoliosListQueryMapping();
            EditePortfolioMapping();
        }

    }
}
