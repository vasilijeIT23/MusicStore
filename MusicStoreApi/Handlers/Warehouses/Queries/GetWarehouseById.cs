using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Warehouses.Queries
{
    public static class GetWarehouseById
    {
        [PublicAPI]
        public class Query : IRequest<Warehouse?>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Warehouse?>
        {
            private readonly IRepository<Warehouse> _repository;

            public RequestHandler(IRepository<Warehouse> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Warehouse?> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetById(request.Id));
            }
        }
    }
}
