using FixIt.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FixIt.Core.Features.Portfolios.Command.Models
{
    public class AddPortfolioCommand : IRequest<Response<string>>
    {


        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public IFormFile ImgUrl { get; set; }

        //WorkerProfile <=> From UserId
        public Guid WorkerProfileId { get; set; }


    }
}
