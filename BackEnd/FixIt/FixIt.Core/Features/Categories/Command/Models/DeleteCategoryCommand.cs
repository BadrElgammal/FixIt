using FixIt.Core.Bases;
using MediatR;

namespace FixIt.Core.Features.Categories.Command.Models
{
    public class DeleteCategoryCommand : IRequest<Response<string>>
    {
        public int CategoryId { get; set; }
        public DeleteCategoryCommand(int id)
        {
            CategoryId = id;
        }
    }
}
