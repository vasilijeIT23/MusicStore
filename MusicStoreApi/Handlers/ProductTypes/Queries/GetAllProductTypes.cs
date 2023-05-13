using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.ProductTypes.Queries
{
    public static class GetAllProductTypes
    {
        [PublicAPI]
        public class Query : IRequest<IEnumerable<ProductType>> { }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, IEnumerable<ProductType>>
        {
            private readonly IRepository<ProductType> _repository;

            public RequestHandler(IRepository<ProductType> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<IEnumerable<ProductType>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetAll());
            }
        }
    }
}
