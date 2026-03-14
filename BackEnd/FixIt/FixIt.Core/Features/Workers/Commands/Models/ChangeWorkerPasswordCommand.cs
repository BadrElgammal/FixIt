using FixIt.Core.Bases;
using MediatR;

namespace FixIt.Core.Features.Workers.Commands.Models
{
    public class ChangeWorkerPasswordCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfarmPassword { get; set; }
    }
}
