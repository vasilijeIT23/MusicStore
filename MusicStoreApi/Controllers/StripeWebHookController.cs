using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Repository;
using MusicStoreApi.Services;
using MusicStoreCore.Entities;
using Stripe;

namespace MusicStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/webhook")]
    public class StripeWebHookController : ControllerBase
    {
        private readonly IRepository<Order> _repository;

        public StripeWebHookController(IRepository<Order> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("email")]
        public async Task<IActionResult> Index()
        {
            
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var emailService = new EmailService();

                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
                    "whsec_ebc85beb7300786443a9c11aef887dcc362f666fc867e998ead50b7c267db42b");

                var charge = (Charge)stripeEvent.Data.Object; 

                if (charge.Status == "succeeded")
                {
                    var orders = _repository.GetAll();
                    //emailService.Send("mucibabic.vasilije21@gmal.com", "Succeeded", "Succeeded");
                    var order = _repository.Find(x => x.Payment.PaymentId == charge.PaymentIntentId).SingleOrDefault();
                    order.PaymentCompleted = true;

                    _repository.SaveChanges();
                }
                else
                {
                    //emailService.Send("mucibabic.vasilije21@gmal.com", "Failed", "Failed");
                    var order = _repository.Find(x => x.Payment.PaymentId == charge.Id).SingleOrDefault();
                    order.PaymentCompleted = false;

                    _repository.SaveChanges();
                }

                return new EmptyResult();
            }
            catch (StripeException e)
            {
                return BadRequest(e);
            }
        }

    }
}
