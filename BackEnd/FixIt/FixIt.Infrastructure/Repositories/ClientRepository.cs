using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Infrastructure.Repositories
{
    public class ClientRepository : GenericRepositoryAsync<User>, IClientRepository
    {
        private readonly FIXITDbContext _context;

        public ClientRepository(FIXITDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<User> GetAllClientsPaginated()
        {
            //return _context.Users.AsNoTracking().Where(u => u.Role.ToLower() == "client").AsQueryable();
            //get all Users (Clients + Workers)
            return _context.Users.AsNoTracking().Where(u => u.Role.ToLower() == "client" || u.Role.ToLower() == "worker").AsQueryable();
        }

        //public async Task<List<Payment>> GetAllPaymentsForUserByUserId(Guid userId)
        //{
        //    return await _context.Payments
        // .Where(p => p.Wallet.UserId == userId)
        // .OrderByDescending(p => p.CreatedAt).ToListAsync();
        //}


    }
}
