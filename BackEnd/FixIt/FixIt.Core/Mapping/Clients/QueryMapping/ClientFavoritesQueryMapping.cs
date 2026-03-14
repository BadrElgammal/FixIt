using FixIt.Core.Features.Clients.Queries.DTOs;
using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Clients
{
    public partial class ClientProfileMapper
    {
        public void GetClientFavoritesMapping()
        {
            CreateMap<Favorite, ClientFavoritesWorkerDTO>()
                .ForMember(dest => dest.FullName, otp => otp.MapFrom(src => src.Worker.User.FullName))
                .ForMember(dest => dest.ImgUrl, otp => otp.MapFrom(src => src.Worker.User.ImgUrl))
                .ForMember(dest => dest.FullName, otp => otp.MapFrom(src => src.Worker.User.FullName))
                .ForMember(dest => dest.Role, otp => otp.MapFrom(src => src.Worker.User.Role))
                .ForMember(dest => dest.City, otp => otp.MapFrom(src => src.Worker.User.City))
                .ForMember(dest => dest.Area, otp => otp.MapFrom(src => src.Worker.Area))
                .ForMember(dest => dest.JobTitle, otp => otp.MapFrom(src => src.Worker.JobTitle))
                .ForMember(dest => dest.Description, otp => otp.MapFrom(src => src.Worker.Description))
                .ForMember(dest => dest.AvailabilityStatus, otp => otp.MapFrom(src => src.Worker.AvailabilityStatus))
                .ForMember(dest => dest.RatingAverage, otp => otp.MapFrom(src => src.Worker.RatingAverage))
                .ForMember(dest => dest.CategoryName, otp => otp.MapFrom(src => src.Worker.Category.CategoryName));


        }
    }
}
