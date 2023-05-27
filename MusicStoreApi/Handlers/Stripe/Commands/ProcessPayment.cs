using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using Stripe;
using Stripe.TestHelpers;
using Customer = Stripe.Customer;
using CustomerService = Stripe.CustomerService;

namespace MusicStoreApi.Handlers.Stripe.Commands
{

    public static class ProcessPayment
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid CustomerId { get; set; }
            public string Currency { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty; 
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<MusicStoreCore.Entities.Customer> _customerRepository;
            private readonly IRepository<MusicStoreCore.Entities.Cart> _cartRepository;

            public RequestHandler(IRepository<MusicStoreCore.Entities.Customer> customerRepository, IRepository<MusicStoreCore.Entities.Cart> cartRepository)
            {
                _customerRepository = customerRepository;
                _cartRepository = cartRepository;
            }
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                StripeConfiguration.ApiKey = "sk_test_51NBHrNGzhsHSoBW7mgFkAv2meSFwdnrlUEA5joYi4Jax76E9MWQmNe9hTES7ioO2FSlDHgRk9EuzGnpaXmjpsIGO00nZHCyxPo";

                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var customerCart = _cartRepository.Find(x => x.Customer.Id == request.CustomerId).SingleOrDefault();
                var storeCustomer = _customerRepository.GetById(request.CustomerId);

                if (storeCustomer == null || customerCart == null)
                {
                    throw new EntityDoesntExistException();
                }

                var stripeCustomer = new CustomerService();
                var chargeService = new ChargeService();

                var customer = new Customer();

                long amount = 0;
                if(storeCustomer.Status == MusicStoreCore.Enums.Status.Advanced && storeCustomer.StatusExpirationDate > DateTime.Now)
                {
                    amount = Convert.ToInt64(customerCart.CartValue * 0.8);
                }
                else
                {
                    amount = Convert.ToInt64(customerCart.CartValue * 0.8);
                }

                var existingCustomer = stripeCustomer.List(new CustomerListOptions { Email = customer.Email }).FirstOrDefault();
                if (existingCustomer != null)
                {
                    customer = existingCustomer;
                }
                else
                {
                    var customerOptions = new CustomerCreateOptions
                    {
                        Name = storeCustomer.FirstName + storeCustomer.LastName,
                        Email = storeCustomer.Email,
                    };
                    customer = stripeCustomer.Create(customerOptions);
                }

                var options = new PaymentIntentCreateOptions
                {
                    Customer = customer.Id,
                    Amount = amount*100,
                    Currency = request.Currency,
                    PaymentMethodTypes = new List<string> { "card" },
                    PaymentMethod = "pm_card_visa",
                    Description = request.Description,
                };
                var service = new PaymentIntentService();
                var charge = service.Create(options);

                if (charge.Id != null)
                {
                    var confirmOptions = new PaymentIntentConfirmOptions { };
                    charge = service.Confirm(
                        charge.Id,
                        confirmOptions
                    );
                }

                if ( charge.Status == "succeeded")
                {
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
        }
    }
}
