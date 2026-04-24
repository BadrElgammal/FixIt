using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Infrastructure.Repositories
{
    public class ChatRoomRepository : GenericRepositoryAsync<ChatRoom>,IChatRoomRepository
    {
        private readonly FIXITDbContext _context;

        public ChatRoomRepository(FIXITDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddAsync(ChatRoom chatRoom)
        {
             await _context.ChatRooms.AddAsync(chatRoom);
            _context.SaveChanges();
        }

        public async Task<ChatRoom?> GetRoomAsync(Guid CurrentUserId, Guid TargetUserId)
        {
            return await _context.ChatRooms.FirstOrDefaultAsync(r => (r.CurrentUserId == CurrentUserId && r.TargetUserId == TargetUserId) || (r.TargetUserId == CurrentUserId && r.CurrentUserId == TargetUserId));
        }

        public async Task<ChatRoom?> GetRoomByIdAsync(int RoomId)
        {
            return await _context.ChatRooms.Where(r => r.RoomId == RoomId).FirstOrDefaultAsync();
        }

        public async Task<List<ChatRoom>> GetUserRooms(Guid userId)
        {
            return await _context.ChatRooms.Include(R => R.CurrentUser).Include(R => R.TargetUser).Where(r => r.CurrentUserId == userId || r.TargetUserId == userId).OrderByDescending(r => r.LastMessageAt).ToListAsync();
        }
    }
}
