using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Payments.Queries;
using MusicStoreApi.Handlers.ProductTypes.Queries;
using MusicStoreCore.Entities;
using System.Net;

namespace MusicStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("filtered")]
        [ProducesResponseType(type: typeof(IEnumerable<Payment>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<ProductType>> Find()
        {
            return NoContent();
        }

        [HttpGet("all")]
        [ProducesResponseType(type: typeof(IEnumerable<Payment>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _mediator.Send(new GetAllPayments.Query());

            return Ok(payments);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(Payment), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await _mediator.Send(new GetPaymentById.Query { Id = id });

            return payment == null ? NotFound() : Ok(payment);
        }
    }
}
