using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;

namespace FixIt.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepositoryAsync<Category>, ICategoryRepository
    {

        private readonly FIXITDbContext _context;

        public CategoryRepository(FIXITDbContext context) : base(context)
        {
            _context = context;
        }





    }
}
