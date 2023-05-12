using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.ProductTypes.Queries
{
    public static class GetProductTypeById
    {
        [PublicAPI]
        public class Query : IRequest<ProductType?>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, ProductType?>
        {
            private readonly IRepository<ProductType> _repository;

            public RequestHandler(IRepository<ProductType> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<ProductType?> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetById(request.Id));
            }
        }
    }
}
