using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Infrastructure.Abstracts
{
    public interface IChatRoomRepository : IGenericRepositoryAsync<ChatRoom>
    {
        Task<ChatRoom?> GetRoomAsync(Guid CurrentUserId, Guid TargetUserId);
        Task<ChatRoom?> GetRoomByIdAsync(int RoomId);
        Task AddAsync(ChatRoom chatRoom);
        Task<List<ChatRoom>> GetUserRooms(Guid userId);
    }
}
