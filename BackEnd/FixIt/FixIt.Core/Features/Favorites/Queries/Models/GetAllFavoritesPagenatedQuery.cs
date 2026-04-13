using FixIt.Core.Features.Favorites.Queries.DTOs;
using FixIt.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Favorites.Queries.Models
{
    public class GetAllFavoritesPagenatedQuery : IRequest<PaginatedResult<ClientFavoritesWorkerPaginatedResponse>>
    {
        public Guid Id { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }
    }
}
