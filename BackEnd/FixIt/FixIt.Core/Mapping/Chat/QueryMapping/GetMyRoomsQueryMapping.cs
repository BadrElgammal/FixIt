using FixIt.Core.Features.Chat.Queries.DTOs;
using FixIt.Core.Features.Service.Queries.DTOs;
using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Chat
{
    public partial class ChatMapping
    {
        public void GetMyRoomsQueryMapping()
        {
            CreateMap<ChatRoom, MyRoomQueryDTO>()
                .ForMember(dest => dest.TargetUserId, otp => otp.MapFrom(src => src.TargetUserId))
                .ForMember(dest => dest.TargetUserName, otp => otp.MapFrom(src => src.TargetUser.FullName))
                .ForMember(dest => dest.TargetUserImgUrl, otp => otp.MapFrom(src => src.TargetUser.ImgUrl))
                .ForMember(dest => dest.TargetUserIsActive, otp => otp.MapFrom(src => src.TargetUser.IsActive));
        }
    }
}