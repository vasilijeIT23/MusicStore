using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Payments.Queries
{
    public static class GetAllPayments
    {
        [PublicAPI]
        public class Query : IRequest<IEnumerable<Payment>> { }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, IEnumerable<Payment>>
        {
            private readonly IRepository<Payment> _repository;

            public RequestHandler(IRepository<Payment> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public Task<IEnumerable<Payment>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetAll());
            }
        }
    }
}
