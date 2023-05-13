using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Products.Commands
{
    public static class DeleteProduct
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Product> _repository;

            public RequestHandler(IRepository<Product> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var product = _repository.GetById(request.Id) ?? throw new ArgumentNullException(nameof(request));

                if (product == null)
                {
                    return Task.FromResult(false);
                }

                _repository.Delete(product);
                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
