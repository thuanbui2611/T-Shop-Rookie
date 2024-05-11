using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.Products.Commands.CreateProduct;
using T_Shop.Application.Features.Products.Commands.DeleteProduct;
using T_Shop.Application.Features.Products.Commands.UpdateProduct;
using T_Shop.Application.Features.Products.Queries.GetProducts;
using T_Shop.Application.Features.Products.Queries.GetProductsById;

namespace T_Shop.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {

            var products = await Mediator.Send(new GetProductQuery());
            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid id)
        {

            var product = await Mediator.Send(new GetProductByIdQuery()
            {
                productId = id
            });

            return Ok(product);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand command)
        {

            var createdProduct = await Mediator.Send(command);
            return Created($"product/{createdProduct.Id}", createdProduct);


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
        {

            command.Id = id;
            var updatedProduct = await Mediator.Send(command);
            return Ok(updatedProduct);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id)
        {

            var result = await Mediator.Send(new DeleteProductCommand()
            {
                Id = id
            });
            return Ok();

        }
    }
}
