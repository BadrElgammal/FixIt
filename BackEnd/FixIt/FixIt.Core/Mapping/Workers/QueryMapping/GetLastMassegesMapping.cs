using FixIt.Core.Features.Workers.Queries.DTOs;
using FixIt.Domain.Entities;

namespace FixIt.Core.Mapping.Workers
{
    public partial class WorkerProfileMapper
    {
        public void GetLastMassegesMapping()
        {
            CreateMap<ChatRoom, MessageDTO>()
                   .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.LastMessage))
                   .ForMember(dest => dest.LastMessageAt, opt => opt.MapFrom(src => src.LastMessageAt))
                  .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.CurrentUser.FullName))
                  .ForMember(dest => dest.SenderImgUrl, opt => opt.MapFrom(src => src.CurrentUser.ImgUrl))
                  .ForMember(dest => dest.TargetUserName, opt => opt.MapFrom(src => src.TargetUser.FullName))
                  .ForMember(dest => dest.TargetUserImgUrl, opt => opt.MapFrom(src => src.TargetUser.ImgUrl));


        }

    }
}
