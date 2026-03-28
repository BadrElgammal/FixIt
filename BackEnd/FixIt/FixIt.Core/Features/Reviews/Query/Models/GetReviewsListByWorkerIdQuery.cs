using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Query.DTOs;
using MediatR;

namespace FixIt.Core.Features.Reviews.Query.Models
{
    public class GetReviewsListByWorkerIdQuery : IRequest<Response<ReviewForWorkerDTO>>
    {
        public Guid workerId { get; set; }
        public GetReviewsListByWorkerIdQuery(Guid workerId)
        {
            this.workerId = workerId;
        }


    }


}
