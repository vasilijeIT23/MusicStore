using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using System.Security.Cryptography;
using System.Text;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public class ChangeCredentials
    {
        [PublicAPI]
        public class Command : IRequest<Customer>
        {
            public string OldPassword { get; set; } = string.Empty;
            public string Username { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Customer>
        {
            private readonly IRepository<Customer> _customerRepository;
            public RequestHandler(IRepository<Customer> customerRepository)
            {
                _customerRepository = customerRepository;
            }
            public Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                if (request.NewPassword == null || request.NewPassword.Length < 8 ||
                    request.Username == null || request.Username.Length < 8) { }

                var existingCustomer = _customerRepository.Find(x => x.Email == request.Email || x.Username == request.Username).SingleOrDefault();

                if (existingCustomer == null)
                {
                    throw new EntityDoesntExistException();
                }

                var newPassword = Hash(request.NewPassword);

                if (existingCustomer.Password == newPassword)
                {
                    throw new RequirementsNotSatisfiedException();
                }

                var salt = existingCustomer.Salt;

                existingCustomer.Username = request.Username;
                existingCustomer.Password = newPassword + salt;

                _customerRepository.SaveChanges();

                return Task.FromResult(existingCustomer);
            }
            public string Hash(string stringToHash)
            {
                return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(stringToHash)));
            }

        }
    }
}
