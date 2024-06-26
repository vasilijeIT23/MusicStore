﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Customers.Commands;
using MusicStoreApi.Handlers.Customers.Queries;
using MusicStoreCore.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace MusicStoreApi.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
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

        [HttpPost("addToCart")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddToCart([FromBody]AddToCart.Command request) 
        {
            var response = await _mediator.Send(request);

            return response == null ? BadRequest() : Ok(response);
        }

        [HttpPut("emptyCart")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EmptyCart([FromBody]EmptyCart.Command request)
        {
            var result = await _mediator.Send(request);

            return result ? Ok(result) : BadRequest();
        }

        [HttpPut("removeCartItem")]
        [ProducesResponseType( (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveCartItem.Command request)
        {
            var response = await _mediator.Send(request);

            return response? Ok(response) : BadRequest();
        }

        [HttpPost("purchase")]
        [ProducesResponseType(type: typeof(Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PurchaseProduct([FromBody] PurchaseFromCart.Command request)
        {
            var response = await _mediator.Send(request);

            return response != null ? Ok(response) : BadRequest();
        }

        [HttpPut("promote")]
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

        [AllowAnonymous]
        [HttpPost("auth")]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateCustomer.Command request)
        {
            var response = await _mediator.Send(request);

            return response != null ? Ok(response) : Unauthorized();  
        }

        [AllowAnonymous]
        [HttpPost("jwtToken")]
        [ProducesResponseType(type: typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GenerateJwtToken([FromBody] GenerateJwtToken.Command request)
        {
            var response = await _mediator.Send(request);

            return response != null ? Ok(response) : BadRequest();
        }

        [HttpPost("credentials")]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeCredentials([FromBody] ChangeCredentials.Command request)
        {
            var response = await _mediator.Send(request);

            return response != null ? Ok(response) : BadRequest();
        }

    }

}
