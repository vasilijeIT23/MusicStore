using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Carts.Queries
{
    public static class GetAllCarts
    {
        [PublicAPI]
        public class Query : IRequest<IEnumerable<Cart>> { }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, IEnumerable<Cart>>
        {
            private readonly IRepository<Cart> _repository;

            public RequestHandler(IRepository<Cart> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<IEnumerable<Cart>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetAll());
            }
        }
    }
}
