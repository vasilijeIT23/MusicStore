using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Orders.Commands;
using MusicStoreApi.Handlers.Orders.Queries;
using MusicStoreCore.Entities;
using System.Net;

namespace MusicStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("filtered")]
        [ProducesResponseType(type: typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Order>> Find()
        {
            return NoContent();
        }

        [HttpGet("all")]
        [ProducesResponseType(type: typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _mediator.Send(new GetAllOrders.Query());

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof( Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _mediator.Send(new GetOrderById.Query { Id = id });

            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Order), (int)HttpStatusCode.OK)]
        public IActionResult Create(Order order)
        {
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Update(Order order)
        {
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromBody] DeleteOrder.Command request)
        {
            var response = await _mediator.Send(request);
            return response ? NoContent() : NotFound();
        }
    }
}
