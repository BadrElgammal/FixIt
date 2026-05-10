using FixIt.Core.Bases;
using MediatR;

namespace FixIt.Core.Features.Users.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }

        public DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}