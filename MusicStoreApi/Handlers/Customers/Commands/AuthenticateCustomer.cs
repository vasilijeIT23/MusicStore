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
        public class Command : IRequest<bool>
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository, IMapper mapper)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                //BadRequest
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var customer = _repository.Find(x => x.Username == request.Username).SingleOrDefault();
                //CustomerDoesntExist
                if (customer != null)
                {
                    return Task.FromResult(UserExtensions.VerifyPassword(request.Password, customer.Password, customer.Salt));
                }

                throw new EntityDoesntExistException();

            }
        }
    }
}
