using FixIt.Core.Features.Clients.Queries.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Clients
{
    public partial class ClientProfileMapper
    {
        public void GetClientProfile()
        {
            CreateMap<User, ClientProfileDTO>();
        }
    }
}
