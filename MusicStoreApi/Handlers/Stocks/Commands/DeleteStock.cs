using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Stocks.Commands
{
    public static class DeleteStock
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Stock> _repository;

            public RequestHandler(IRepository<Stock> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var stock = _repository.GetById(request.Id) ?? throw new ArgumentNullException(nameof(request));

                if (stock == null)
                {
                    return Task.FromResult(false);
                }

                _repository.Delete(stock);
                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
