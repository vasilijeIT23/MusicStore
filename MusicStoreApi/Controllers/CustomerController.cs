using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Customers.Commands;
using MusicStoreApi.Handlers.Customers.Queries;
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
        public async Task<IActionResult> GetAll()
        {
            var customers = await _mediator.Send(new GetAllCustomers.Query());

            return Ok(customers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _mediator.Send(new GetCustomerById.Query { Id = id });

            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateCustomer.Command request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateCustomer.Command request)
        {
            var response = await _mediator.Send(request);
            //todo add notfound somehow

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomer.Command request)
        {
            var response = await _mediator.Send(request);
            return response ? NoContent() : NotFound();
        }

        [HttpPost("{customerId}/add/{productId}/to/{cartId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddToCart([FromBody]AddToCart.Command request) 
        {
            var response = await _mediator.Send(request);

            return response == null ? BadRequest() : Ok(response);
        }

        [HttpPut("{customerId}/emptyCart/{cartId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EmptyCart([FromBody]EmptyCart.Command request)
        {
            var result = await _mediator.Send(request);

            return result ? Ok(result) : BadRequest();
        }

        [HttpPut("{customerId}/removeCartItem/{cartItemId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveCartItem.Command request)
        {
            var response = await _mediator.Send(request);

            return response? Ok(response) : BadRequest();
        }

        [HttpPost("{customerId}/purchase/{cartId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PurchaseProduct([FromBody] PurchaseFromCart.Command request)
        {
            var response = await _mediator.Send(request);

            return response == null ? Ok(response) : BadRequest();
        }

        [HttpPut("{id}/promote")]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Promote([FromBody] PromoteCustomer.Command request)
        {
            var response = await _mediator.Send(request);

            return response == null ? BadRequest() : Ok(response);
        }

        [HttpPost("{customerId}/review/{productId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult ReviewProduct([FromRoute] Guid customerId, [FromRoute] Guid productId)
        {
            return NoContent();
        }
    }

}
