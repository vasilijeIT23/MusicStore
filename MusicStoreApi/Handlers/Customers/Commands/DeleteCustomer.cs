using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public static class DeleteCustomer
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Customer> _customerRepository;
            private readonly IRepository<Cart> _cartRepository;

            public RequestHandler(IRepository<Customer> customerRepsoitory, IRepository<Cart> cartRepository)
            {
                _customerRepository = customerRepsoitory ?? throw new ArgumentNullException(nameof(customerRepsoitory));
                _cartRepository= cartRepository ?? throw new ArgumentNullException(nameof(_cartRepository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var customer = _customerRepository.GetById(request.Id);
                var cart = _cartRepository.Find(x => x.Customer.Id == request.Id).SingleOrDefault();

                if (customer == null || cart == null)
                {
                    return Task.FromResult(false);
                }

                _cartRepository.Delete(cart);
                _customerRepository.Delete(customer);
                _customerRepository.SaveChanges();
                _cartRepository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
