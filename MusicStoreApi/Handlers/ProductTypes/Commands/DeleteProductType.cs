using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.ProductTypes.Commands
{
    public static class DeleteProductType
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<ProductType> _repository;

            public RequestHandler(IRepository<ProductType> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var productType = _repository.GetById(request.Id) ?? throw new ArgumentNullException(nameof(request));

                if (productType == null)
                {
                    return Task.FromResult(false);
                }

                _repository.Delete(productType);
                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
