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
            var WorksList = await _mediator.Send(new GetCategoriesListQuery());
            return Ok(WorksList);
        }

        //AddCategory
        [HttpPost("AddCategory")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddCategoryCommand command)
        {
            var WorksList = await _mediator.Send(command);
            return Ok(WorksList);
        }

        //update
        [HttpPut("EditeCategory/{id}")]
        [Authorize]
        public async Task<IActionResult> Edite([FromRoute] int id)
        {
            var WorksList = await _mediator.Send(new EditeCategoryCommand(id));
            return Ok(WorksList);
        }

        //Delete
        [HttpDelete("DeleteCategory/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var WorksList = await _mediator.Send(new DeleteCategoryCommand(id));
            return Ok(WorksList);
        }

    }
}
