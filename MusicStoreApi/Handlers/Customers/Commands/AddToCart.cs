using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using MusicStoreCore.Enums;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public static class AddToCart
    {
        [PublicAPI]
        public class Command : IRequest<CartItem>
        {
            public Guid CustomerId { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }

        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, CartItem>
        {
            private readonly IRepository<Customer> _customerRepository;
            private readonly IRepository<Product> _productRepository;
            private readonly IRepository<Cart> _cartRepository;
            private readonly IRepository<Stock> _stockRepository;
            private readonly IRepository<CartItem> _cartItemRepository;

            public RequestHandler(IRepository<Customer> customerRepository, IRepository<Product> productRepository, IRepository<Cart> cartRepsoitory, IRepository<Stock> stockRepository, IRepository<CartItem> cartItemRepository)
            {
                _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                _cartRepository = cartRepsoitory ?? throw new ArgumentNullException(nameof(cartRepsoitory));
                _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));
                _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            }
            public Task<CartItem> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
                var customer = _customerRepository.GetById(request.CustomerId);
                var product = _productRepository.GetById(request.ProductId);
                var cart = _cartRepository.Find(x => x.Customer.Id == request.CustomerId).SingleOrDefault();
                var stock = _stockRepository.Find(x => x.Product.Id == request.ProductId).SingleOrDefault();

                if (customer == null || product == null || cart == null || stock == null)
                {
                    throw new EntityDoesntExistException();
                }

                if (!product.InStock || request.Quantity > stock.Quantity)
                {
                    throw new RequirementsNotSatisfiedException();
                }

                if(cart.CartItems.SingleOrDefault(x => x.Product == product) != null)
                {
                    var cartItemm = cart.CartItems.SingleOrDefault(x => x.Product.Id == product.Id);
                    cartItemm.Quantity += request.Quantity;
                    cart.CartValue += product.Price * request.Quantity;

                    _cartItemRepository.SaveChanges();
                    _cartRepository.SaveChanges();

                    return Task.FromResult(cartItemm);
                }
                // todo add cartitem expiration date
                var cartItem = new CartItem(product, cart, request.Quantity);
                cart.CartItems.Add(cartItem);
                cart.CartValue += product.Price * request.Quantity;
  
                _cartItemRepository.SaveChanges();
                _cartRepository.SaveChanges();

                return Task.FromResult(cartItem);
            }
        }
    }
}
