using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public static class CreateCustomer
    {
        [PublicAPI]
        public class Command : IRequest<Customer>
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Customer>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository)
            {
                _repository = repository;
            }
            public Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var existingCustomer = _repository.Find(x => x.Email == request.Email).SingleOrDefault();

                if (existingCustomer != null)
                {
                    return Task.FromResult(existingCustomer);
                }

                var customer = new Customer(request.FirstName, request.LastName, request.Email);

                _repository.Create(customer);
                _repository.SaveChanges();
                return Task.FromResult(customer);
            }
        }
    }
}
