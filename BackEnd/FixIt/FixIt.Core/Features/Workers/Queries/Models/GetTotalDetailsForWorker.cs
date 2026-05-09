using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Workers.Queries.Models
{
    public class GetTotalDetailsForWorker : IRequest<Response<TotalDTO>>
    {
        public Guid userId { get; set; }

        public GetTotalDetailsForWorker(Guid id)
        {
            userId = id;
        }



    }
}
