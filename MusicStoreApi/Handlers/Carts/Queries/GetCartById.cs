using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Carts.Queries
{
    public static class GetCartById
    {
        [PublicAPI]
        public class Query : IRequest<Cart?>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Cart?>
        {
            private readonly IRepository<Cart> _repository;

            public RequestHandler(IRepository<Cart> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Cart?> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetById(request.Id));
            }
        }
    }
}
