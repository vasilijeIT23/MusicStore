using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Carts.Commands
{
    public static class DeleteCart
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Cart> _repository;

            public RequestHandler(IRepository<Cart> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var cart = _repository.GetById(request.Id) ?? throw new ArgumentNullException(nameof(request));

                if (cart == null)
                {
                    return Task.FromResult(false);
                }

                _repository.Delete(cart);
                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
