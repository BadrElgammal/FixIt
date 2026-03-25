using FixIt.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FixIt.Core.Features.Portfolios.Command.Models
{
    public class EditePortfolioCommand : IRequest<Response<string>>
    {

        public int PortfolioId { get; set; }

        public EditePortfolioCommand()
        {

        }

        public EditePortfolioCommand(int id)
        {
            PortfolioId = id;
        }


        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        public IFormFile? ImgUrl { get; set; }


        //WorkerProfile <=> From UserId
        public Guid WorkerProfileId { get; set; }

    }
}
