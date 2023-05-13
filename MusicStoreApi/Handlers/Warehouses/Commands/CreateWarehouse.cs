using JetBrains.Annotations;
using MediatR;
using MovieStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Warehouses.Commands
{
    public static class CreateWarehouse
    {
        [PublicAPI]
        public class Command : IRequest<Warehouse>
        {
            public string Name { get; set; } = string.Empty;
            public int Capacity { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Warehouse>
        {
            private readonly IRepository<Warehouse> _repository;

            public RequestHandler(IRepository<Warehouse> repository)
            {
                _repository = repository;
            }
            public Task<Warehouse> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                Warehouse warehouse = new Warehouse()
                {
                    Name = request.Name,
                    Capacity = request.Capacity
                };

                _repository.Create(warehouse);
                _repository.SaveChanges();

                return Task.FromResult(warehouse);
            }
        }
    }
}
