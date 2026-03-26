using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Query.DTOs;
using MediatR;

namespace FixIt.Core.Features.Reviews.Query.Models
{
    public class GetMyAllReviewsListQuery : IRequest<Response<List<ReviewDTO>>>
    {

        public Guid userId { get; set; }
        public GetMyAllReviewsListQuery(Guid userId)
        {
            this.userId = userId;
        }

    }
}
