using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Orders.Commands
{
    public static class DeleteOrder
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Order> _repository;

            public RequestHandler(IRepository<Order> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var order = _repository.GetById(request.Id) ?? throw new ArgumentNullException(nameof(request));

                if (order == null)
                {
                    return Task.FromResult(false);
                }

                _repository.Delete(order);
                _repository.SaveChanges();

                return Task.FromResult(true);
            }
        }
    }
}
