using FixIt.Core.Features.Workers.Queries.Results;
using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Workers
{
    public partial class WorkerProfileMapper
    {
        public void GetWorkerListMapping()
        {

            CreateMap<WorkerProfile, GetWorkersResponce>()
                        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                        .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.User.ImgUrl))
                        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))
                        .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City));
   
    
        }           
    

    }
}
