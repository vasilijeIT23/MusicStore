using Microsoft.AspNetCore.Mvc;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/productTypes")]
    public class ProductTypeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductType>> Find()
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
        public IActionResult Create(ProductType productType )
        {
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update(ProductType productType)
        {
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(ProductType productType)
        {
            return NoContent();
        }
    }
}
