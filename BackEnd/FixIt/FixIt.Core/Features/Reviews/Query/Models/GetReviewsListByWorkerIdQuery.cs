using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Query.DTOs;
using FixIt.Core.Wrapper;
using MediatR;
using System.Text.Json.Serialization;

namespace FixIt.Core.Features.Reviews.Query.Models
{
    public class GetReviewsListByWorkerIdQuery : IRequest<PaginatedResult<ReviewForWorkerDTO>>
    {
        
        public Guid workerId { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }

    }


}
