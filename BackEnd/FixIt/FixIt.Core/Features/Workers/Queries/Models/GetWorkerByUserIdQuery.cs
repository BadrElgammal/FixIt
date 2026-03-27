using FixIt.Core.Features.Workers.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Workers.Queries.Models
{
    public class GetWorkerByUserIdQuery : IRequest<Bases.Response<GetSingleWorkerResponce>>//WorkerProfile
    {

        public Guid userId { get; set; }

        public GetWorkerByUserIdQuery(Guid id)
        {
            userId = id;
        }

    }
}
