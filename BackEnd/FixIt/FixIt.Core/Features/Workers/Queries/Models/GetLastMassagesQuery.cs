using FixIt.Core.Bases;
using FixIt.Core.Features.Workers.Queries.DTOs;
using MediatR;

namespace FixIt.Core.Features.Workers.Queries.Models
{
    public class GetLastMassagesQuery : IRequest<Response<List<MessageDTO>>>
    {
        public Guid UserId { get; set; }

        public GetLastMassagesQuery(Guid id)
        {
            UserId = id;
        }

        public int? SelectedNumber { get; set; }


    }
}
