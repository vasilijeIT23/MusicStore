using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Stocks.Commands
{
    public static class CreateStock
    {
        [PublicAPI]
        public class Command : IRequest<Stock>
        {
            public Guid ProductId { get; set; }
            public Guid WarehouseId { get; set; }
            public int Quantity { get; set; }
            public int Capacity { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Stock>
        {
            private readonly IRepository<Stock> _stockRepository;
            private readonly IRepository<Warehouse> _warehouseRepository;
            private readonly IRepository<Product> _productRepository;

            public RequestHandler(IRepository<Stock> stockRepository, IRepository<Warehouse> warehouseRepository, IRepository<Product> productRepository)
            {
                _productRepository= productRepository;
                _warehouseRepository= warehouseRepository;
                _stockRepository = stockRepository;
            }
            public Task<Stock> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }
                
                var product = _productRepository.GetById(request.ProductId);
                var warehouse = _warehouseRepository.GetById(request.WarehouseId);

                if(product == null || warehouse == null) 
                {
                    throw new EntityDoesntExistException();
                }

                if(request.Quantity > warehouse.Capacity)
                {
                    throw new RequirementsNotSatisfiedException();
                }

                warehouse.Capacity -= request.Quantity;

                var stock = new Stock(product, warehouse, request.Quantity);

                _stockRepository.Create(stock);
                _stockRepository.SaveChanges();
                _warehouseRepository.SaveChanges();

                return Task.FromResult(stock);
            }

        }
    }
}
