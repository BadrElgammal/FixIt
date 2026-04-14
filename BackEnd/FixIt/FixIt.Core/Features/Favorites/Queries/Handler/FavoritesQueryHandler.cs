using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Favorites.Queries.DTOs;
using FixIt.Core.Features.Favorites.Queries.Models;
using FixIt.Core.Wrapper;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using FixIt.Service.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Favorites.Queries.Handler
{
    public class FavoritesQueryHandler : ResponseHandler,
        IRequestHandler<GetAllFavoritesPagenatedQuery, PaginatedResult<ClientFavoritesWorkerPaginatedResponse>>
    {

        private readonly IMapper _mapper;
        private readonly IFavoritesService _favoritesService;

        public FavoritesQueryHandler(IMapper mapper, IFavoritesService favoritesService)
        {
            _mapper = mapper;
            _favoritesService = favoritesService;
        }

        public async Task<PaginatedResult<ClientFavoritesWorkerPaginatedResponse>> Handle(GetAllFavoritesPagenatedQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Favorite, ClientFavoritesWorkerPaginatedResponse>> expression = e => new ClientFavoritesWorkerPaginatedResponse(e.WorkerId, e.Worker.User.FullName, e.Worker.User.ImgUrl, e.Worker.User.Role, e.Worker.User.City, e.Worker.Area, e.Worker.JobTitle, e.Worker.Description, e.Worker.AvailabilityStatus, e.Worker.RatingAverage, e.Worker.Category.CategoryName);
            var query = _favoritesService.GetAllFavoritesByUserIdPaginated(request.Id);
            var PaginatedList = await query.Select(expression).ToPaginatedListAsync(request.pageNum, request.pageSize);
            return PaginatedList;
        }
    }

}
