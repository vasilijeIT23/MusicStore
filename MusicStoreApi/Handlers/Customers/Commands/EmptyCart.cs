using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public static class EmptyCart
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid CustomerId { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Customer> _customerRepository;
            private readonly IRepository<Cart> _cartRepository;
            private readonly IRepository<CartItem> _cartItemRepository;

            public RequestHandler(IRepository<Customer> customerRepository, IRepository<Cart> cartRepsoitory, IRepository<CartItem> cartItemRepository)
            {
                _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
                _cartRepository = cartRepsoitory ?? throw new ArgumentNullException(nameof(cartRepsoitory));
                _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));
            }
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
                var customer = _customerRepository.GetById(request.CustomerId);
                var cart = _cartRepository.Find(x => x.Customer.Id == request.CustomerId).SingleOrDefault();

                if (customer == null || cart == null)
                {
                    throw new EntityDoesntExistException();
                }

                cart.CartValue = 0;
                cart.CartItems.Clear();


                _cartItemRepository.SaveChanges();
                _cartRepository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
