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
    public class ClientRepository : GenericRepositoryAsync<User>, IClientRepository
    {
        private readonly FIXITDbContext _context;

        public ClientRepository(FIXITDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<User> GetAllClientsPaginated()
        {
            return _context.Users.AsNoTracking().Where(u => u.Role.ToLower() == "client").AsQueryable();
        }
    }
}
