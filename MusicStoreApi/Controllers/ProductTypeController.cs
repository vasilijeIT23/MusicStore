using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Customers.Queries;
using MusicStoreCore.Entities;
using System.Net;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/productTypes")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductTypeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("filtered")]
        [ProducesResponseType(type: typeof(IEnumerable<ProductType>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<ProductType>> Find()
        {
            return NoContent();
        }

        [HttpGet("all")]
        [ProducesResponseType(type: typeof(IEnumerable<ProductType>), (int)HttpStatusCode.OK)]
        public  async Task<IActionResult> GetAll()
        {
            var productTypes = await _mediator.Send(new GetAllCustomers.Query());

            return Ok(productTypes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(ProductType), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productType = await _mediator.Send(new GetCustomerById.Query { Id = id });

            return productType == null ? NotFound() : Ok(productType);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(ProductType), (int)HttpStatusCode.OK)]
        public IActionResult Create(ProductType productType )
        {
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(ProductType), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Update(ProductType productType)
        {
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Delete(ProductType productType)
        {
            return NoContent();
        }
    }
}
