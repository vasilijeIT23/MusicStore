using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Orders.Queries
{
    public static class GetAllOrders
    {
        [PublicAPI]
        public class Query : IRequest<IEnumerable<Order>> { }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, IEnumerable<Order>>
        {
            private readonly IRepository<Order> _repository;

            public RequestHandler(IRepository<Order> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<IEnumerable<Order>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetAll());
            }
        }
    }
}
