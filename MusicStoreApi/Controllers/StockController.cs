using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Stocks.Commands;
using MusicStoreApi.Handlers.Stocks.Queries;
using MusicStoreCore.Entities;
using System.Net;

namespace MusicStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("filtered")]
        [ProducesResponseType(type: typeof(IEnumerable<Stock>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Stock>> Find()
        {
            return NoContent();
        }

        [HttpGet("all")]
        [ProducesResponseType(type: typeof(IEnumerable<Stock>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllStock.Query());

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(Stock), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetStockById.Query { Id = id });

            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Stock), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateStock.Command request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(Stock), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateStock.Command request)
        {
            var response = await _mediator.Send(request);

            return response == true ? Ok(response) : NotFound();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromBody] DeleteStock.Command request)
        {
            var response = await _mediator.Send(request);
            return response ? NoContent() : NotFound();
        }
    }
}
