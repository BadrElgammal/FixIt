using FixIt.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FixIt.Core.Features.Portfolios.Command.Models
{
    public class EditePortfolioCommand : IRequest<Response<string>>
    {
        public int PortfolioId { get; set; }
        public EditePortfolioCommand(int id)
        {
            PortfolioId = id;
        }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public string ImgUrl { get; set; }

    }
}
