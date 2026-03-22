using FixIt.API.Base;
using FixIt.Core.Features.Categories.Command.Models;
using FixIt.Core.Features.Categories.Query.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : AppController
    {

        //GetAll
        [HttpGet("AllCategories")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var respose = await _mediator.Send(new GetCategoriesListQuery());
            return Ok(respose);
        }

        //AddCategory
        [HttpPost("AddCategory")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddCategoryCommand command)
        {
            var respose = await _mediator.Send(command);
            return Ok(respose);
        }

        //update
        [HttpPut("EditeCategory/{id}")]
        [Authorize]
        public async Task<IActionResult> Edite([FromRoute] int id)
        {
            var respose = await _mediator.Send(new EditeCategoryCommand(id));
            return Ok(respose);
        }

        //Delete
        [HttpDelete("DeleteCategory/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var respose = await _mediator.Send(new DeleteCategoryCommand(id));
            return Ok(respose);
        }

    }
}
