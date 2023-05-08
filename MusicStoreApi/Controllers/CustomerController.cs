using Microsoft.AspNetCore.Mvc;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Find()
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
        public IActionResult Create(Customer customer)
        {
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update(Customer customer)
        {
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Customer customer)
        {
            return NoContent();
        }

        [HttpPost]
        public IActionResult AddToCart(Customer customer) 
        {
            return NoContent();
        }

        [HttpPost("{customerId}/purchase/{cartId}")]
        public IActionResult PurchaseProduct([FromRoute] Guid customerId, [FromRoute] Guid cartId)
        {
            return NoContent();
        }

        [HttpPut("{id}/promote")]
        public IActionResult Promote([FromRoute] Guid id)
        {
            return NoContent();
        }
    }

}
