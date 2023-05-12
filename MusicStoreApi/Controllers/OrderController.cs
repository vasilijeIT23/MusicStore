using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStoreCore.Entities;
using System.Net;

namespace MusicStoreApi.Controllers
{
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
        [ProducesResponseType(type: typeof(IEnumerable<Warehouse>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof( Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetById(Guid Id)
        {
            return NoContent();
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
        public IActionResult Delete(Order order)
        {
            return NoContent();
        }
    }
}
