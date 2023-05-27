using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Stripe.Commands;
using MusicStoreCore.Entities;
using Stripe;
using System.Net;
using Customer = Stripe.Customer;

namespace MusicStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/payment")]
    public class StripeApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StripeApiController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost]
        [ProducesResponseType(type: typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPayment.Command request)
        {
            var response = await _mediator.Send(request);

            return response == true ? Ok(response) : BadRequest();
        }

        [HttpPost("createCustomer")]
        [ProducesResponseType(type: typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateStripeCustomer([FromBody] CreateStripeCustomer.Command request)
        {
            var response = await _mediator.Send(request);

            return response != null ? Ok(response) : BadRequest();
        }

        [HttpPost("createIntent")]
        [ProducesResponseType(type: typeof(PaymentIntent), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntent.Command request)
        {
            var response = await _mediator.Send(request);

            return response != null ? Ok(response) : BadRequest();
        }

        [HttpPost("confirmIntent")]
        [ProducesResponseType(type: typeof(PaymentIntent), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmPaymentIntent([FromBody] ConfirmPaymentIntent.Command request)
        {
            var response = await _mediator.Send(request);

            return response != null ? Ok(response) : BadRequest();
        }
    }
}
