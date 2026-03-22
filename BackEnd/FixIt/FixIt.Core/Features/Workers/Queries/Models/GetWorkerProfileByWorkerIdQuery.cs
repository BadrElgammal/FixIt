using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Workers.Queries.Models
{
    public class GetWorkerProfileByWorkerIdQuery : IRequest<Response<WorkerProfileDTO>>
    {
        public Guid WorkerId { get; set; }
        public GetWorkerProfileByWorkerIdQuery(Guid WorkerId)
        {
            this.WorkerId = WorkerId;
        }
    }
}
