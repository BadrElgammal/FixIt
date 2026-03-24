using FixIt.Core.Features.Service.Queries.DTOs;
using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Service
{
    public partial class ServiceMapping
    {
        public void GetServiceRequest()
        {
            CreateMap<ServiceRequest, ServiceRequestDetailsDTO>()
                .ForMember(dest => dest.ServiceId, otp => otp.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.ClientName, otp => otp.MapFrom(src => src.Client.FullName))
                .ForMember(dest => dest.WorkerName, otp => otp.MapFrom(src => src.Worker.User.FullName))
                .ForMember(dest => dest.ReviewId, otp => otp.MapFrom(src => src.Review.ReviewId))
                .ForMember(dest => dest.Rate, otp => otp.MapFrom(src => src.Review.Rate))
                .ForMember(dest => dest.Comment, otp => otp.MapFrom(src => src.Review.Comment))
                .ForMember(dest => dest.CreatedAt, otp => otp.MapFrom(src => src.Review.CreatedAt));
        }
    }
}