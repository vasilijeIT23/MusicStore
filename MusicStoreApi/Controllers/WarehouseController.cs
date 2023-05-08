using Microsoft.AspNetCore.Mvc;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    public class WarehouseController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Warehouse>> Find()
        {
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid Id)
        {
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(Warehouse warehouse)
        {
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update(Warehouse warehouse)
        {
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Warehouse warehouse)
        {
            return NoContent();
        }
    }
}
