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
    public class ChatMessageRepository : GenericRepositoryAsync<ChatMessage> , IChatMessageRepository
    {
        private readonly FIXITDbContext _context;

        public ChatMessageRepository(FIXITDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(int roomId)
        {
            return await _context.ChatMessages.Include(m => m.Sender).Where(m => m.RoomId == roomId).OrderBy(m => m.CreatedAt).ToListAsync();
        }

        public async Task AddAsync(ChatMessage chatMessage)
        {
            await _context.AddAsync(chatMessage);
            _context.SaveChanges();
        }
    }
}
