using Microsoft.AspNetCore.Mvc;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Find()
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
        public IActionResult Create(Order order)
        {
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update(Order order)
        {
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Order order)
        {
            return NoContent();
        }
    }
}
