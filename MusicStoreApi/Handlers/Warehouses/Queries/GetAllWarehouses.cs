using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Warehouses.Queries
{
    public static class GetAllWarehouses
    {
        [PublicAPI]
        public class Query : IRequest<IEnumerable<Warehouse>> { }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, IEnumerable<Warehouse>>
        {
            private readonly IRepository<Warehouse> _repository;

            public RequestHandler(IRepository<Warehouse> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<IEnumerable<Warehouse>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetAll());
            }
        }
    }
}
