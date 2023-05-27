using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Options;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using Stripe;

namespace MusicStoreApi.Handlers.Stripe.Commands
{
    public static class ConfirmPaymentIntent
    {
        [PublicAPI]
        public class Command : IRequest<PaymentIntent>
        {
            public string Id { get; set; } = string.Empty;
            public string CustomerId { get; set; } = string.Empty;
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, PaymentIntent>
        {
            private readonly IRepository<MusicStoreCore.Entities.Customer> _customerRepository;

            public RequestHandler(IRepository<MusicStoreCore.Entities.Customer> customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public Task<PaymentIntent> Handle(Command request, CancellationToken cancellationToken)
            {
                //StripeConfiguration.ApiKey = "sk_test_51NBHrNGzhsHSoBW7mgFkAv2meSFwdnrlUEA5joYi4Jax76E9MWQmNe9hTES7ioO2FSlDHgRk9EuzGnpaXmjpsIGO00nZHCyxPo";

                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var service = new PaymentIntentService();
                var confirmedIntent = new PaymentIntent();

                var storeCustomer = _customerRepository.GetById(Guid.Parse(request.CustomerId));

                var confirmOptions = new PaymentIntentConfirmOptions { };
                confirmedIntent = service.Confirm(
                    request.Id,
                    confirmOptions
                );

                if (confirmedIntent.Status == "succeeded")
                {
                    if(storeCustomer != null)
                    {
                        storeCustomer.MoneySpent += confirmedIntent.Amount/100;
                        _customerRepository.SaveChanges();
                    }
                    return Task.FromResult(confirmedIntent);
                }

                return Task.FromResult(confirmedIntent);
            }
        }
    }
}
