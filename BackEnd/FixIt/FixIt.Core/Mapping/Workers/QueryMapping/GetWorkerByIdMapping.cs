using FixIt.Core.Features.Workers.Queries.DTOs;
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
        public void GetWorkerByIdMapping()
        {
       



        CreateMap<WorkerProfile, GetSingleWorkerResponce>()
                        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                        .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                        .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.User.ImgUrl))
                        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))
                        .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
                        .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.User.LastLogin))
                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt))
                        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.User.UpdatedAt))
                        .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive));

        }

    }
}
