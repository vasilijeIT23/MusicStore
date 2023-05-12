using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Customers.Queries;
using MusicStoreCore.Entities;
using System.Net;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("filtered")]
        [ProducesResponseType(type: typeof(IEnumerable<Cart>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Cart>> Find()
        {
            return NoContent();
        }

        [HttpGet("all")]
        [ProducesResponseType(type: typeof(IEnumerable<Cart>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var carts = await _mediator.Send(new GetAllCustomers.Query());

            return Ok(carts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(Cart), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cart = await _mediator.Send(new GetCustomerById.Query { Id = id });

            return cart == null ? NotFound() : Ok(cart);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Cart), (int)HttpStatusCode.OK)]
        public IActionResult Create(Cart cart)
        {
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(Cart), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Update(Cart cart)
        {
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Delete(Cart cart)
        {
            return NoContent();
        }
    }
}
