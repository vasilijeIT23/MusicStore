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
            private readonly IRepository<Product> _productRepository;
            private readonly IRepository<Stock> _stockRepository;

            public RequestHandler(IRepository<Product> productRepository, IRepository<Stock> stockRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository)); 
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var product = _productRepository.GetById(request.Id) ?? throw new ArgumentNullException(nameof(request));
                var stock = _stockRepository.Find(x =>x.Product.Id == request.Id).SingleOrDefault();

                if (product == null || stock == null)
                {
                    return Task.FromResult(false);
                }

                _productRepository.Delete(product);
                _stockRepository.Delete(stock);

                _productRepository.SaveChanges();
                _stockRepository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
