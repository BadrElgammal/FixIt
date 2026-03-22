using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Query.DTOs;
using MediatR;

namespace FixIt.Core.Features.Reviews.Query.Models
{
    public class GetReviewsListQuery : IRequest<Response<List<ReviewDTO>>>
    {


    }
}
