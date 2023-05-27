using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Payments.Queries
{
    public static class GetPaymentById
    {
        [PublicAPI]
        public class Query : IRequest<Payment?>
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Payment?>
        {
            private readonly IRepository<Payment> _repository;

            public RequestHandler(IRepository<Payment> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<Payment?> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetById(request.Id));
            }
        }
    }
}
