using JetBrains.Annotations;
using MediatR;
using MovieStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using MusicStoreCore.Enums;

namespace MusicStoreApi.Handlers.ProductTypes.Commands
{
    public static class CreateProductType
    {
        [PublicAPI]
        public class Command : IRequest<ProductType>
        {
            public string Name { get; set; } = string.Empty;
            public Category Category { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, ProductType>
        {
            private readonly IRepository<ProductType> _repository;

            public RequestHandler(IRepository<ProductType> repository)
            {
                _repository = repository;
            }
            public Task<ProductType> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                if (_repository.Find(x => x.Name == request.Name).SingleOrDefault() != null)
                {
                    throw new InvalidInputValueException();
                }

                ProductType productType = new ProductType()
                {
                    Name = request.Name,
                    Category = request.Category
                };

                _repository.Create(productType);
                _repository.SaveChanges();

                return Task.FromResult(productType);
            }
        }
    }
}
