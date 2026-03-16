using FixIt.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FixIt.Core.Features.Categories.Command.Models
{
    public class EditeCategoryCommand : IRequest<Response<string>>
    {
        public int CategoryId { get; set; }
        public EditeCategoryCommand(int id)
        {
            CategoryId = id;
        }

        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
