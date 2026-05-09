using FixIt.Core.Features.Workers.Queries.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Workers
{
    public partial class WorkerProfileMapper
    {
        public void GetLastServicesMapping()
        {
            CreateMap<ServiceRequest, ServiceDTO>()
                  .ForMember(dest => dest.ServiceTitle, opt => opt.MapFrom(src => src.ServiceTitle))
                  .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                  .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                  .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
                  .ForMember(dest => dest.ClientImgUrl, opt => opt.MapFrom(src => src.Client.ImgUrl))
                  .ForMember(dest => dest.WorkerName, opt => opt.MapFrom(src => src.Worker.User.FullName))
                  .ForMember(dest => dest.WorkerImgUrl, opt => opt.MapFrom(src => src.Worker.User.ImgUrl));



        }


    }
}
