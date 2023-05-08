using Microsoft.AspNetCore.Mvc;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Cart>> Find()
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
        public IActionResult Create(Cart cart)
        {
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update(Cart cart)
        {
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Cart cart)
        {
            return NoContent();
        }

        [HttpPut]
        public IActionResult EmptyCart(Cart cart) 
        {
            return NoContent();        
        }
    }
}
