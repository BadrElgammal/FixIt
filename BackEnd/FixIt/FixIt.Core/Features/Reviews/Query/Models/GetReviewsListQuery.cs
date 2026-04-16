using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Query.DTOs;
using FixIt.Core.Wrapper;
using MediatR;

namespace FixIt.Core.Features.Reviews.Query.Models
{
    public class GetReviewsListQuery : IRequest<PaginatedResult<ReviewDTO>>
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }

    }
}
