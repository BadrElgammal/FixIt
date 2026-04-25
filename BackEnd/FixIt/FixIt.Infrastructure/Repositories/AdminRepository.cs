using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;

namespace FixIt.Infrastructure.Repositories
{
    public class AdminRepository : GenericRepositoryAsync<User>, IAdminRepository
    {
        private readonly FIXITDbContext _db;

        public AdminRepository(FIXITDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
