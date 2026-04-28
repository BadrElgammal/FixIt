using FixIt.Core.Bases;
using MediatR;

namespace FixIt.Core.Features.Admin.Command.Models
{
    public class BlockByAdminCommand : IRequest<Response<string>>
    {
        public Guid id { get; set; }
        public BlockByAdminCommand(Guid _id)
        {
            id = _id;
        }
    }
}
