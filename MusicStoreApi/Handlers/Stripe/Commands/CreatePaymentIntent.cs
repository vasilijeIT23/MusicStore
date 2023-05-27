using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using Stripe;
using Customer = Stripe.Customer;

namespace MusicStoreApi.Handlers.Stripe.Commands
{
    public static class CreatePaymentIntent
    {
        [PublicAPI]
        public class Command : IRequest<PaymentIntent>
        {
            public Customer Customer { get; set; } = null!;
            public Guid Id { get; set; }
            public string Currency { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public Guid OrderId { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, PaymentIntent>
        {
            private readonly IRepository<MusicStoreCore.Entities.Customer> _customerRepository;
            private readonly IRepository<Payment> _paymentRepository;
            private readonly IRepository<Order> _orderRepository;


            public RequestHandler(IRepository<MusicStoreCore.Entities.Customer> customerRepository
                , IRepository<Payment> paymentRepository
                , IRepository<Order> orderRepository)
            {
                _customerRepository = customerRepository;
                _paymentRepository = paymentRepository;
                _orderRepository = orderRepository;
            }
            public Task<PaymentIntent> Handle(Command request, CancellationToken cancellationToken)
            {
                StripeConfiguration.ApiKey = "sk_test_51NBHrNGzhsHSoBW7mgFkAv2meSFwdnrlUEA5joYi4Jax76E9MWQmNe9hTES7ioO2FSlDHgRk9EuzGnpaXmjpsIGO00nZHCyxPo";

                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var storeCustomer = _customerRepository.GetById(request.Id);
                var order = _orderRepository.GetById(request.OrderId);

                if (storeCustomer == null || order == null)
                {
                    throw new EntityDoesntExistException();
                }

                long amount = Convert.ToInt64(order.Price);

                var options = new PaymentIntentCreateOptions
                {
                    Customer = request.Customer.Id,
                    Amount = amount * 100,
                    Currency = request.Currency,
                    PaymentMethodTypes = new List<string> { "card" },
                    PaymentMethod = "pm_card_visa",
                    Description = request.Description,
                };

                var service = new PaymentIntentService();
                var charge = service.Create(options);

                charge.Metadata.Add("storeCustomer", storeCustomer.Id.ToString());

                var newPayment = new Payment(charge.Id, charge.CustomerId, charge.Amount);

                order.Payment = newPayment;

                _paymentRepository.SaveChanges();
                _orderRepository.SaveChanges();

                return Task.FromResult(charge);
            }
        }
    }
}
