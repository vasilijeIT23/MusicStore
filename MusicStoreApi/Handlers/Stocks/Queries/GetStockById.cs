using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Stocks.Queries
{
    public class GetStockById
    {
        [PublicAPI]
        public class Query : IRequest<Stock?>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Stock?>
        {
            private readonly IRepository<Stock> _repository;

            public RequestHandler(IRepository<Stock> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Stock?> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetById(request.Id));
            }
        }

    }
}
