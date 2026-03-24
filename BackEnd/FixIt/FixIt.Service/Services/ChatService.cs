using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRoomRepository _roomRepo;
        private readonly IChatMessageRepository _messageRepo;
        private readonly IService<User> _userservice;

        public ChatService(IChatRoomRepository roomRepo , IChatMessageRepository messageRepo , IService<User> userservice)
        {
            _roomRepo = roomRepo;
            _messageRepo = messageRepo;
            _userservice = userservice;
        }

        public async Task<int> GetOrCreateRoom(Guid currentUserId, Guid targerUserId)
        {
            var room = await _roomRepo.GetRoomAsync(currentUserId, targerUserId);
            if (room != null)
                return room.RoomId;
            room = new ChatRoom
            {
                CurrentUserId = currentUserId,
                TargetUserId = targerUserId
            };
            await _roomRepo.AddAsync(room);
            return room.RoomId;
        }
        public async Task<List<ChatMessage>> GetMessages(int roomId)
        {
            return await _messageRepo.GetMessagesAsync(roomId);
        }


        public async Task<List<ChatRoom>> GetUserRooms(Guid userId)
        {
            return await _roomRepo.GetUserRooms(userId);
        }

        public async Task AddMessage(ChatMessage chatMessage)
        {
            await _messageRepo.AddAsync(chatMessage);
        }
        public async Task UpdateRoom(ChatRoom chatRoom)
        {
            await _roomRepo.UpdateAsync(chatRoom);
        }

        public async Task<ChatRoom> GetRoomByRoomId(int RoomId)
        {
            return await _roomRepo.GetRoomByIdAsync(RoomId);
        }


        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return _userservice.Find(predicate);
        }
    }
}
