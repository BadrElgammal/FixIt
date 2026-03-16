using FixIt.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FixIt.Core.Features.Categories.Command.Models
{
    public class AddCategoryCommand : IRequest<Response<string>>
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
