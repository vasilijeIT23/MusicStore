using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using Stripe;
using Customer = Stripe.Customer;
using CustomerService = Stripe.CustomerService;

namespace MusicStoreApi.Handlers.Stripe.Commands
{
    public static class CreateStripeCustomer
    {
        [PublicAPI]
        public class Command : IRequest<Customer>
        {
            public Guid CustomerId { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Customer>
        {
            private readonly IRepository<MusicStoreCore.Entities.Customer> _customerRepository;

            public RequestHandler(IRepository<MusicStoreCore.Entities.Customer> customerRepository)
            {
                _customerRepository = customerRepository;
            }
            public Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                StripeConfiguration.ApiKey = "sk_test_51NBHrNGzhsHSoBW7mgFkAv2meSFwdnrlUEA5joYi4Jax76E9MWQmNe9hTES7ioO2FSlDHgRk9EuzGnpaXmjpsIGO00nZHCyxPo";

                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var storeCustomer = _customerRepository.GetById(request.CustomerId);

                if (storeCustomer == null)
                {
                    throw new EntityDoesntExistException();
                }

                var stripeCustomer = new CustomerService();

                var customer = new Customer();

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

                return Task.FromResult(customer);
            }
        }
    }
}
