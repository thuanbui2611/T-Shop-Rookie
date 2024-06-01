using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.ProductReview.Queries.GetReviewsOfProduct;
using T_Shop.Application.Features.Products.Commands.CreateProduct;
using T_Shop.Application.Features.Products.Commands.DeleteProduct;
using T_Shop.Application.Features.Products.Commands.UpdateProduct;
using T_Shop.Application.Features.Products.Queries.GetProducts;
using T_Shop.Application.Features.Products.Queries.GetProductsById;
using T_Shop.Extensions;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Product.QueryModel;
using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ApiControllerBase
    {
        /// <summary>
        /// Acquire products information
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet]
        public async Task<ActionResult<List<ProductResponseModel>>> GetProductsAsync(
            [FromQuery] PaginationRequestModel pagination,
            [FromQuery] ProductQuery productQuery
            )
        {
            var (products, paginationMetaData) = await Mediator.Send(new GetProductsQuery()
            {
                ProductQuery = productQuery,
                Pagination = pagination
            });

            Response.AddPaginationHeader(paginationMetaData);
            return Ok(products);
        }

        /// <summary>
        /// Acquire product information by identification
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseModel>> GetProductByIdAsync([FromRoute] Guid id)
        {

            var product = await Mediator.Send(new GetProductByIdQuery()
            {
                productId = id
            });

            return Ok(product);

        }

        /// <summary>
        /// Acquire reviews of product information
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("review/{productId}")]
        public async Task<ActionResult<List<ProductReviewResponseModel>>> GetProductReviewsByProductId(
            [FromRoute] Guid productId,
            [FromQuery] PaginationRequestModel pagination)
        {
            var (reviews, paginationMetaData) = await Mediator.Send(new GetReviewsOfProductQuery()
            {
                ProductID = productId,
                Pagination = pagination

            });
            Response.AddPaginationHeader(paginationMetaData);
            return Ok(reviews);

        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="201">Successfully created item.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPost]
        public async Task<ActionResult<ProductResponseModel>> CreateProductAsync([FromForm] CreateProductCommand command)
        {
            var createdProduct = await Mediator.Send(command);
            return Created($"product/{createdProduct.Id}", createdProduct);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully updated item.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponseModel>> UpdateProductAsync([FromRoute] Guid id, [FromForm] UpdateProductCommand command)
        {
            command.Id = id;
            var updatedProduct = await Mediator.Send(command);
            return Ok(updatedProduct);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="204">Successfully deleted item.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id)
        {

            await Mediator.Send(new DeleteProductCommand()
            {
                Id = id
            });
            return NoContent();

        }
    }
}
