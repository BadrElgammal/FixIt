using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Infrastructure.Repositories
{
    public class FavoritesRepository : GenericRepositoryAsync<Favorite>, IFavoritesRepository
    {
        private readonly FIXITDbContext _context;
        public FavoritesRepository(FIXITDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Favorite>> GetAllFavoritesByUserId(object userId)
        {
            return await _context.Favorites.Include(f => f.Worker).
                                            ThenInclude(w => w.User).
                                          Include(f => f.Worker).
                                            ThenInclude(w => w.Category)
                                          .Where(f => f.ClientId == (Guid)userId).ToListAsync();
        }

        public async Task<Favorite> GetFavoriteByClientIdAndWorkerId(Guid clientId, Guid WorkerId)
        {
            return await _context.Favorites
                         .FirstOrDefaultAsync(f => f.WorkerId == WorkerId && f.ClientId == clientId);
        }


    }
}
