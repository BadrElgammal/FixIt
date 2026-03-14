using FixIt.Core.Bases;
using MediatR;

namespace FixIt.Core.Features.Workers.Commands.Models
{
    public class DeleteWorkerCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public DeleteWorkerCommand(Guid Id)
        {
            this.Id = Id;
        }


    }
}
