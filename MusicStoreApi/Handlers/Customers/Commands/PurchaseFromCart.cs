using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public static class PurchaseFromCart
    {
        [PublicAPI]
        public class Command : IRequest<Order>
        {
            public Guid CustomerId { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Order>
        {
            private readonly IRepository<Customer> _customerRepository;
            private readonly IRepository<Order> _orderRepository;
            private readonly IRepository<OrderItem> _orderItemRepository;
            private readonly IRepository<Cart> _cartRepository;
            private readonly IRepository<CartItem> _cartItemRepository;

            public RequestHandler(IRepository<CartItem> cartItemRepository, IRepository<Customer> customerRepository, IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository, IRepository<Cart> cartRepository)
            {
                _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));
                _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
                _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
                _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
                _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            }
            public Task<Order> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null) throw new ArgumentNullException();

                var customer = _customerRepository.GetById(request.CustomerId);
                
                if (customer == null)
                {
                    throw new InvalidInputValueException();
                }

                var cart = _cartRepository.Find(x => x.Customer.Id == request.CustomerId).SingleOrDefault();

                if (cart == null)
                {
                    throw new EntityDoesntExistException();
                }
                
                if(cart.CartItems.Count() == 0)
                {
                    throw new RequirementsNotSatisfiedException();
                }

                double orderPrice = 0;
                var order = new Order(customer);

                foreach(var item in cart.CartItems)
                {
                    var orderItem = new OrderItem(item.Product, order, item.Quantity);
                    order.OrderItems.Add(orderItem);
                    orderPrice += item.Quantity*item.Product.Price;
                    _orderItemRepository.SaveChanges();
                }

                order.Price = orderPrice;
                _orderRepository.SaveChanges();

                cart.CartItems.Clear();
                _cartRepository.SaveChanges();

                return Task.FromResult(order);
            }
        }
    }
}
