using FixIt.Core.Bases;
using FixIt.Core.Features.Reviews.Query.DTOs;
using FixIt.Core.Wrapper;
using MediatR;
using System.Text.Json.Serialization;

namespace FixIt.Core.Features.Reviews.Query.Models
{
    public class GetMyAllReviewsListQuery : IRequest<PaginatedResult<ReviewDTO>>
    {
        [JsonIgnore]
        public Guid userId { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }
    }
}
