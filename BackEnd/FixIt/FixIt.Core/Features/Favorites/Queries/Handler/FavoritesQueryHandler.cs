using AutoMapper;
using FixIt.Core.Bases;
using FixIt.Core.Features.Favorites.Queries.DTOs;
using FixIt.Core.Features.Favorites.Queries.Models;
using FixIt.Service.Abstracts;
using FixIt.Service.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Core.Features.Favorites.Queries.Handler
{
    public class FavoritesQueryHandler : ResponseHandler,
        IRequestHandler<GetAllFavoritesQuery, Response<List<ClientFavoritesWorkerDTO>>>
    {

        private readonly IMapper _mapper;
        private readonly IFavoritesService _favoritesService;

        public FavoritesQueryHandler(IMapper mapper, IFavoritesService favoritesService)
        {
            _mapper = mapper;
            _favoritesService = favoritesService;
        }



        public async Task<Response<List<ClientFavoritesWorkerDTO>>> Handle(GetAllFavoritesQuery request, CancellationToken cancellationToken)
        {
            var Favorites = await _favoritesService.GetAllFavoritesByUserId(request.Id);
            var FavoritesMapper = _mapper.Map<List<ClientFavoritesWorkerDTO>>(Favorites);
            return Success(FavoritesMapper);
        }
    }

}
