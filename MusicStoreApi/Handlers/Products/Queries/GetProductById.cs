using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Products.Queries
{
    public static class GetProductById
    {
        [PublicAPI]
        public class Query : IRequest<Product?>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Product?>
        {
            private readonly IRepository<Product> _repository;

            public RequestHandler(IRepository<Product> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Product?> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetById(request.Id));
            }
        }
    }
}
