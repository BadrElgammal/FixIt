using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Workers.Queries.Models
{
    public class GetLastReviewsQuery : IRequest<Response<List<LastReviewDTO>>>
    {

        public Guid userId { get; set; }

        public GetLastReviewsQuery(Guid id)
        {
            userId = id;
        }

        public int? SelectedNumber { get; set; }

    }
}
