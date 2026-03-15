using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Infrastructure.Repositories
{
    public class ReviewsRepository : GenericRepositoryAsync<Review>, IReviewsRepository
    {
        private readonly FIXITDbContext _context;
        public ReviewsRepository(FIXITDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
