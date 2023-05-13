using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Handlers.Warehouses.Commands;
using MusicStoreApi.Handlers.Warehouses.Queries;
using MusicStoreCore.Entities;
using System.Net;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WarehouseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("filtered")]
        [ProducesResponseType(type: typeof(IEnumerable<Warehouse>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Warehouse>> Find()
        {
            return NoContent();
        }

        [HttpGet("all")]
        [ProducesResponseType(type: typeof(IEnumerable<Warehouse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var warehouses = await _mediator.Send(new GetAllWarehouses.Query());

            return Ok(warehouses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof( Warehouse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var warehouse = await _mediator.Send(new GetWarehouseById.Query { Id = id });

            return warehouse == null ? NotFound() : Ok(warehouse);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Warehouse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateWarehouse.Command request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(Warehouse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Update(Warehouse warehouse)
        {
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromBody] DeleteWarehouse.Command request)
        {
            var response = await _mediator.Send(request);
            return response ? NoContent() : NotFound();
        }
    }
}
