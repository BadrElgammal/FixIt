using AutoMapper;

namespace FixIt.Core.Mapping.Admin
{
    public partial class AdminMapper : Profile
    {
        public AdminMapper()
        {
            GetAdminProfileQueryMapping();

            EditeAdminCommandMapping();

        }

    }
}
