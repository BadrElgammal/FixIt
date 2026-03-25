using FixIt.Core.Features.Service.Commands.Models;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Service
{
    public partial class ServiceMapping
    {
        public void CreateServieRequestMapping()
        {
            CreateMap<CreateServiceRequestCommand, ServiceRequest>()
                .ForMember(dest => dest.RequestedImgUrl, otp => otp.Ignore());


        }
    }
}
