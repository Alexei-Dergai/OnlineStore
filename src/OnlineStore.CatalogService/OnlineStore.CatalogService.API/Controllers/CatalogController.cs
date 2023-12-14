using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.CatalogService.Application.Commands;
using OnlineStore.CatalogService.Application.Queries;
using OnlineStore.CatalogService.Application.Responses;
using OnlineStore.CatalogService.Domain.Specs;
using System.Net;

namespace OnlineStore.CatalogService.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("products/{id}")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("products")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductByProductName(string product)
        {
            var query = new GetProductByNameQuery(product);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("all-products")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(typeof(IList<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CategoryResponse>>> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("application-types")]
        [ProducesResponseType(typeof(IList<ApplicationTypeResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ApplicationTypeResponse>>> GetAllApplicationTypes()
        {
            var query = new GetAllApplicationTypeQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("product/{category}")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductsByCategoryName(string categoryName)
        {
            var query = new GetProductByCategoryQuery(categoryName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("product")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }

        [HttpPut]
        [Route("product")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }
        [HttpDelete]
        [Route("product")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var query = new DeleteProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }   
}
