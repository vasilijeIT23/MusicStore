using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using MusicStoreCore.Enums;

namespace MusicStoreApi.Handlers.Products.Commands
{
    public static class CreateProduct
    {
        [PublicAPI]
        public class Command : IRequest<Product>
        {
            public string Name { get; set; } = string.Empty!;
            public double Price { get; set; }
            public Guid ProductType { get; set; }
            public string ImagePath { get; set; } = string.Empty;

        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Product>
        {
            private readonly IRepository<Product> _productRepository;
            private readonly IRepository<ProductType> _productTypeRepository;

            public RequestHandler(IRepository<Product> productRepository, IRepository<ProductType> productTypeRepository)
            {
                _productRepository = productRepository;
                _productTypeRepository = productTypeRepository;
            }
            public Task<Product> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                if (_productRepository.Find(x => x.Name == request.Name).SingleOrDefault() != null)
                {
                    throw new InvalidInputValueException();
                }

                var productType = _productTypeRepository.GetById(request.ProductType);

                if (productType == null)
                {
                    throw new InvalidInputValueException();
                }

                var product = new Product(request.Name, request.Price, productType, request.ImagePath);

                product.InStock = true;
                _productRepository.Create(product);
                _productRepository.SaveChanges();

                return Task.FromResult(product);
            }
        }
    }
}
