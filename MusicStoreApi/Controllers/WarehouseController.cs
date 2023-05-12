using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof( Warehouse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetById(Guid Id)
        {
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(Warehouse), (int)HttpStatusCode.OK)]
        public IActionResult Create(Warehouse warehouse)
        {
            return NoContent();
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
        public IActionResult Delete(Warehouse warehouse)
        {
            return NoContent();
        }
    }
}
