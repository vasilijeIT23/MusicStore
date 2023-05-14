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

            public RequestHandler(IRepository<CartItem> cartItemRepository)
            {
                _cartItemRepository = cartItemRepository ?? throw new ArgumentNullException(nameof(cartItemRepository));
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

                _cartItemRepository.Delete(cartItem);
                _cartItemRepository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
