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

        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Product>
        {
            private readonly IRepository<Product> _productRepository;
            private readonly IRepository<ProductType> _productTypeRepository;
            private readonly IRepository<Stock> _stockRepository;
            private readonly IRepository<Warehouse> _warehouseRepository;


            public RequestHandler(IRepository<Product> productRepository, IRepository<ProductType> productTypeRepository, IRepository<Stock> stockRepository, IRepository<Warehouse> warehouseRepository)
            {
                _productRepository = productRepository;
                _productTypeRepository = productTypeRepository;
                _stockRepository = stockRepository;
                _warehouseRepository = warehouseRepository;
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
                var warehouse = _warehouseRepository.GetAll().SingleOrDefault();

                if (productType == null || warehouse == null)
                {
                    throw new InvalidInputValueException();
                }

                var product = new Product(request.Name, request.Price, productType);
                var stock = new Stock(product, warehouse, 100);

                _productRepository.Create(product);
                _stockRepository.Create(stock);
                _productRepository.SaveChanges();
                _stockRepository.SaveChanges();

                return Task.FromResult(product);
            }
        }
    }
}
