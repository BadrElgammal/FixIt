using FixIt.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FixIt.Core.Features.Portfolios.Command.Models
{
    public class AddPortfolioImgURlCommand : IRequest<Response<string>>
    {
        public int PortfolioId { get; set; }
        public AddPortfolioImgURlCommand(int id)
        {
            PortfolioId = id;
        }

        [Required]
        public IFormFile ImgUrl { get; set; }
    }
}
