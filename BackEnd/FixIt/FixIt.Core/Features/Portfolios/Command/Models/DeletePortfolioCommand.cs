using FixIt.Core.Bases;
using MediatR;

namespace FixIt.Core.Features.Portfolios.Command.Models
{
    public class DeletePortfolioCommand : IRequest<Response<string>>
    {
        public int PortfolioId { get; set; }
        public DeletePortfolioCommand(int id)
        {
            PortfolioId = id;
        }


    }
}
