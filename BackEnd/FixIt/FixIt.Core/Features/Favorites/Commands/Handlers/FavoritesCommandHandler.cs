using FixIt.Core.Bases;
using FixIt.Core.Features.Favorites.Commands.Models;
using FixIt.Domain.Entities;
using FixIt.Service.Abstracts;
using MediatR;

namespace FixIt.Core.Features.Favorites.Commands.Handlers
{
    public class FavoritesCommandHandler : ResponseHandler,
        IRequestHandler<DeleteFavoriteCommand, Response<String>>,
        IRequestHandler<AddFavoriteCommand, Response<String>>
    {

        private readonly IFavoritesService _favoritesService;

        public FavoritesCommandHandler(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }


        public async Task<Response<string>> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var fav = await _favoritesService.GetFavoriteByClientIdAndWorkerId(request.ClientId, request.WorkerId);
            if (fav == null) return NotFound<string>();

            var result = await _favoritesService.DeleteFavorite(fav);
            if (result == "success") return Success("تم الحذف ");
            else return NotFound<string>();

        }

        public async Task<Response<string>> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
        {
            var fav = await _favoritesService.GetFavoriteByClientIdAndWorkerId(request.ClientId, request.WorkerId);
            if (fav != null) return BadRequest<string>("العامل موجود بالفعل ضمن المفضلين ");

            var newFav = new Favorite()
            {
                ClientId = request.ClientId,
                WorkerId = request.WorkerId

            };


            var result = await _favoritesService.AddFavorite(newFav);
            if (result == "success") return Success("تم الاضافة ");
            else return NotFound<string>();
        }
    }
}
