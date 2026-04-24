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
                .ForMember(dest => dest.TargetUserId, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var userId = context.Items["UserId"]; 
                    return src.TargetUserId.Equals(userId) ? src.CurrentUserId : src.TargetUserId;
                }))
                .ForMember(dest => dest.TargetUserName, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var userId = context.Items["UserId"];
                    return src.TargetUserId.Equals(userId) ? src.CurrentUser.FullName : src.TargetUser.FullName;
                }))
                .ForMember(dest => dest.TargetUserImgUrl, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var userId = context.Items["UserId"];
                    return src.TargetUserId.Equals(userId) ? src.CurrentUser.ImgUrl : src.TargetUser.ImgUrl;
                }))
                .ForMember(dest => dest.TargetUserIsActive, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    var userId = context.Items["UserId"];
                    return src.TargetUserId.Equals(userId) ? src.CurrentUser.IsActive : src.TargetUser.IsActive;
                }));
        }
    }
}