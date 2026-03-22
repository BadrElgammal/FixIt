using FixIt.API.Base;
using FixIt.Core.Features.Favorites.Commands.Models;
using FixIt.Core.Features.Favorites.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : AppController
    {


        [HttpGet("ClientFavorites")]
        [Authorize]
        public async Task<IActionResult> ClientFavorites()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            var respose = await _mediator.Send(new GetAllFavoritesQuery(Id));
            return NewResult(respose);
        }

        //add Fav

        [HttpPost("AddFavorite/{WorkerId}")]
        [Authorize]
        public async Task<IActionResult> AddFavorite(Guid WorkerId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);


            var command = new AddFavoriteCommand(WorkerId)
            {
                ClientId = Id
            };


            var respose = await _mediator.Send(command);
            return NewResult(respose);
        }


        //Delete Fav
        [HttpDelete("DeleteFavorite/{WorkerId}")]
        [Authorize]
        public async Task<IActionResult> DeleteFavorite(Guid WorkerId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);

            var command = new DeleteFavoriteCommand(WorkerId)
            {
                ClientId = Id
            };



            var respose = await _mediator.Send(command);
            return NewResult(respose);
        }





    }
}
