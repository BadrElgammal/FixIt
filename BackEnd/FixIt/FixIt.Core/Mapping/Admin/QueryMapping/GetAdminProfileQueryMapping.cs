using FixIt.Core.Features.Admin.Query.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Admin
{
    public partial class AdminMapper
    {
        public void GetAdminProfileQueryMapping()
        {
            CreateMap<User, AdminProfileDTO>();
        }

    }
}
