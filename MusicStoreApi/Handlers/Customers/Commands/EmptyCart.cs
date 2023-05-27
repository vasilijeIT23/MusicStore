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
            private readonly IRepository<Stock> _stockRepository;

            public RequestHandler(IRepository<Customer> customerRepository, IRepository<Cart> cartRepsoitory, IRepository<CartItem> cartItemRepository, IRepository<Stock> stockRepository)
            {
                _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
                _cartRepository = cartRepsoitory ?? throw new ArgumentNullException(nameof(cartRepsoitory));
                _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));
                _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
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

                foreach(var item in cart.CartItems)
                {
                    var stock = _stockRepository.Find(x => x.Product.Id == item.Product.Id).FirstOrDefault();

                    if (stock == null)
                    {
                        throw new EntityDoesntExistException();
                    }

                    stock.Quantity += item.Quantity;
                }

                cart.CartValue = 0;
                cart.CartItems.Clear();


                _cartItemRepository.SaveChanges();
                _cartRepository.SaveChanges();
                _stockRepository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
