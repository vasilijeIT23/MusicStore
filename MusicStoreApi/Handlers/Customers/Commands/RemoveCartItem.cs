using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public static class RemoveCartItem
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid CartItemId { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<CartItem> _cartItemRepository;
            private readonly IRepository<Cart> _cartRepository;
            private readonly IRepository<Stock> _stockRepository;

            public RequestHandler(IRepository<CartItem> cartItemRepository, IRepository<Cart> cartRepository, IRepository<Stock> stockRepository)
            {
                _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));
                _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(_cartRepository));
                _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(_stockRepository));
            }
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
                var cartItem = _cartItemRepository.Find(x => x.Id == request.CartItemId).SingleOrDefault();

                if (cartItem == null)
                {
                    throw new EntityDoesntExistException();
                }

                var stock = _stockRepository.Find(x => x.Product.Id == cartItem.Product.Id).FirstOrDefault();

                if (stock == null)
                {
                    throw new EntityDoesntExistException();
                }

                var cart = _cartRepository.GetById(cartItem.Cart.Id);

                if (cart == null)
                {
                    throw new EntityDoesntExistException();
                }

                stock.Quantity += cartItem.Quantity;

                cart.CartValue -= cartItem.Quantity * cartItem.Product.Price;
                _cartItemRepository.Delete(cartItem);
                
                _cartItemRepository.SaveChanges();
                _cartRepository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
