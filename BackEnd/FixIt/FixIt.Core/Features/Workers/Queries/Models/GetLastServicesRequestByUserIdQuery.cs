using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Workers.Queries.Models
{
    public class GetLastServicesRequestByUserIdQuery : IRequest<Response<List<ServiceDTO>>>
    {

        public Guid userId { get; set; }

        public GetLastServicesRequestByUserIdQuery(Guid id)
        {
            userId = id;
        }

        public int? SelectedNumber { get; set; }

    }
}
