using FixIt.Core.Bases;
using MediatR;

namespace FixIt.Core.Features.Admin.Command.Models
{
    public class DeleteAdminCommand : IRequest<Response<string>>
    {
        public Guid userId { get; set; }

        public DeleteAdminCommand(Guid id)
        {
            userId = id;
        }

    }
}
