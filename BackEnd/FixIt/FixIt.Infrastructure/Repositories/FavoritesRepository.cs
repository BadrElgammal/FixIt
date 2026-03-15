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
    public class FavoritesRepository : GenericRepositoryAsync<Favorite> , IFavoritesRepository
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
    }
}
