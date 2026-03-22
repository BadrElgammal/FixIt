using FixIt.Core.Features.Workers.Queries.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Workers
{
    public partial class WorkerProfileMapper
    {
        public void GetWorkerProfileByWorkerIdMapping()
        {
            CreateMap<WorkerProfile, WorkerProfileDTO>()
                     .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                     .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));

        }

    }
}
