using FixIt.Core.Bases;
using MediatR;

namespace FixIt.Core.Features.Reviews.Command.Models
{
    public class DeleteReviewCommand : IRequest<Response<string>>
    {
        public int ReviewId { get; set; }

        public DeleteReviewCommand(int id)
        {
            ReviewId = id;
        }

    }
}
