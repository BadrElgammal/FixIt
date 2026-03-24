using FixIt.Core.Features.Workers.Commands.Models;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Workers
{
    public partial class WorkerProfileMapper
    {
        public void EditeWorkerCommandMapping()
        {
            CreateMap<EditeWorkerCommand, WorkerProfile>()
               .ForPath(dest => dest.User.FullName, opt => opt.MapFrom(src => src.FullName))
               .ForPath(dest => dest.User.Phone, opt => opt.MapFrom(src => src.Phone))
               .ForPath(dest => dest.User.City, opt => opt.MapFrom(src => src.City));



        }

    }
}
