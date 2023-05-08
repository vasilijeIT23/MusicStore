using Microsoft.AspNetCore.Mvc;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase 
    {
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Find()
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
        public IActionResult Create(Product product)
        {
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Product product)
        {
            return NoContent();
        }
    }
}
