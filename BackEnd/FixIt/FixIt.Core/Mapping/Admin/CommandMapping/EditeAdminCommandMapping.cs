using FixIt.Core.Features.Admin.Command.Models;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Admin
{
    public partial class AdminMapper
    {
        public void EditeAdminCommandMapping()
        {
            CreateMap<EditeAdminCommand, User>();


        }

    }
}
