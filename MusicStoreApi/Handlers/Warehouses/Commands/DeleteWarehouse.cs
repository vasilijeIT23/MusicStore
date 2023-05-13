using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Warehouses.Commands
{
    public static class DeleteWarehouse
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Warehouse> _repository;

            public RequestHandler(IRepository<Warehouse> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var warehouse = _repository.GetById(request.Id) ?? throw new ArgumentNullException(nameof(request));

                if (warehouse == null)
                {
                    return Task.FromResult(false);
                }

                _repository.Delete(warehouse);
                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
