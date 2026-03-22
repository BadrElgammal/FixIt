using FixIt.Core.Features.Portfolios.Command.Models;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Portfolios
{
    public partial class PortfolioMapper
    {
        public void AddPortfolioMapping()
        {
            CreateMap<AddPortfolioCommand, Portfolio>();


        }

    }
}
