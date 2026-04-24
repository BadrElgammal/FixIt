using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Mapping.Chat
{
    public partial class ChatMapping : Profile
    {
        public ChatMapping()
        {
            GetMyRoomsQueryMapping();
            GetRoomMessagesQueryMapping();
        }
    }
}
