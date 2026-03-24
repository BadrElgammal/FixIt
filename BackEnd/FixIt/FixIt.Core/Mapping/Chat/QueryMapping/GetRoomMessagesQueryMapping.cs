using FixIt.Core.Features.Chat.Queries.DTOs;
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
        public void GetRoomMessagesQueryMapping()
        {
            CreateMap<ChatMessage, MessageQueryDTO>()
              .ForMember(dest => dest.SenderName, otp => otp.MapFrom(src => src.Sender.FullName))
              .ForMember(dest => dest.SenderImgUrl, otp => otp.MapFrom(src => src.Sender.ImgUrl))
              .ForMember(dest => dest.SenderIsActive, otp => otp.MapFrom(src => src.Sender.IsActive));
        }
    }
}
