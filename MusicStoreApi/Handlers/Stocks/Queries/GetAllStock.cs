using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Stocks.Queries
{
    public static class GetAllStock
    {
        [PublicAPI]
        public class Query : IRequest<IEnumerable<Stock>> { }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, IEnumerable<Stock>>
        {
            private readonly IRepository<Stock> _repository;

            public RequestHandler(IRepository<Stock> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<IEnumerable<Stock>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetAll());
            }
        }
    }
}
