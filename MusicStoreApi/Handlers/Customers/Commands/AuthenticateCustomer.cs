using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using MusicStoreCore.Enums;
using MusicStoreApi.Helpers;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public static class AuthenticateCustomer
    {
        [PublicAPI]
        public class Command : IRequest<Customer>
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Customer>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository, IMapper mapper)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }
            public Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                //BadRequest
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var customer = _repository.Find(x => x.Username == request.Username).SingleOrDefault();
                //CustomerDoesntExist
                if (customer != null && UserExtensions.VerifyPassword(request.Password, customer.Password, customer.Salt))
                {
                    return Task.FromResult(customer);
                }

                throw new EntityDoesntExistException();

            }
        }
    }
}
