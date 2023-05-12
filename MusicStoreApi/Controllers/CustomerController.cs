using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStoreCore.Entities;
using System.Net;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("filtered")]
        [ProducesResponseType(type: typeof(IEnumerable<Customer>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Customer>> Find()
        {
            return NoContent();
        }

        [HttpGet("all")]
        [ProducesResponseType(type: typeof(IEnumerable<Customer>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetById(Guid Id)
        {
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        public IActionResult Create(Customer customer)
        {
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Update(Customer customer)
        {
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Delete(Customer customer)
        {
            return NoContent();
        }

        [HttpPost("{customerId}/add/{cartId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult AddToCart(Customer customer) 
        {
            return NoContent();
        }

        [HttpPut("{customerId}/remove/{cartId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult EmptyCart(Cart cart)
        {
            return NoContent();
        }

        [HttpPut("{customerId}/removeItem/{cartItemId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult RemoveFromCart(Cart cart)
        {
            return NoContent();
        }

        [HttpPost("{customerId}/purchase/{cartId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult PurchaseProduct([FromRoute] Guid customerId, [FromRoute] Guid cartId)
        {
            return NoContent();
        }

        [HttpPut("{id}/promote")]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Promote([FromRoute] Guid id)
        {
            return NoContent();
        }
    }

}
