using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.Categories.Commands.CreateCategory;
using T_Shop.Application.Features.Categories.Commands.DeleteCategory;
using T_Shop.Application.Features.Categories.Commands.UpdateCategory;
using T_Shop.Application.Features.Categories.Queries.GetCategories;
using T_Shop.Application.Features.Categories.Queries.GetCategoryById;

namespace T_Shop.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ApiControllerBase
    {
        /// <summary>
        /// Acquire Categories information
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {

            var categories = await Mediator.Send(new GetCategoriesQuery());
            return Ok(categories);
        }

        /// <summary>
        /// Acquire Categories information by identification
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid id)
        {

            var category = await Mediator.Send(new GetCategoryByIdQuery()
            {
                Id = id
            });
            return Ok(category);
        }

        /// <summary>
        /// Create a category
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="201">Successfully created item.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryCommand command)
        {

            var category = await Mediator.Send(command);
            return Created($"category/{category.Id}", category);
        }
        /// <summary>
        /// Update a category
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully updated item.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] Guid id, [FromBody] UpdateCategoryCommand command)
        {

            command.Id = id;
            var category = await Mediator.Send(command);
            return Created($"category/{category.Id}", category);
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="204">Successfully deleted item.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id)
        {

            var result = await Mediator.Send(new DeleteCategoryCommand()
            {
                Id = id
            });
            return Ok();
        }
    }
}
