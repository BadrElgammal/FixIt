using FixIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Abstracts
{
    public interface IChatService
    {
        Task<int> GetOrCreateRoom(Guid currentUserId, Guid targerUserId);
        Task AddMessage(ChatMessage chatMessage);
        Task UpdateRoom(ChatRoom chatRoom);
        Task<List<ChatMessage>> GetMessages(int roomId);
        Task<List<ChatRoom>> GetUserRooms(Guid userId);
        Task<ChatRoom> GetRoomByRoomId(int RoomId);
        IEnumerable<User> Find(Expression<Func<User, bool>> predicate);
    }
}
