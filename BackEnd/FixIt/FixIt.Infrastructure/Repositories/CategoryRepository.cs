using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FixIt.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepositoryAsync<Category>, ICategoryRepository
    {

        private readonly FIXITDbContext _context;

        public CategoryRepository(FIXITDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetCategoryByNameAsync(string Name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == Name);
        }


    }
}
