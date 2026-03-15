using FixIt.API.Base;
using FixIt.Core.Features.Favorites.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : AppController
    {

        private readonly IMediator _mediator;

        public FavoriteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Favorites")]
        [Authorize]
        public async Task<IActionResult> ClientFavorites()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid Id = Guid.Parse(userId);
            var respose = await _mediator.Send(new GetAllFavoritesQuery(Id));
            return NewResult(respose);
        }
    }
}
