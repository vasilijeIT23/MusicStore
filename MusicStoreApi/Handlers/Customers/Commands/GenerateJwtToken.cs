using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Helpers;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public class GenerateJwtToken
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            public string Username { get; set; } = string.Empty;
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, bool>
        {
            private readonly IConfiguration _configuration;
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository, IConfiguration configuration)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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
                    string? key = _configuration["JWT:Key"];
                    if (key != null)
                    {
                        Claim[]? claims = null;
                        if (customer != null)
                        {
                            claims = new[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                                new Claim(ClaimTypes.Name, customer.Username.ToString()),
                                new Claim(ClaimTypes.Role, customer.Role.ToString()),
                                new Claim(ClaimTypes.GivenName, customer.FirstName),
                                new Claim(ClaimTypes.Surname, customer.LastName),
                                new Claim(ClaimTypes.Email, customer.Email)
                            };
                        }

                        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
                        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new(_configuration["JWT:Issuer"],
                                                _configuration["JWT:Audience"],
                                                claims,
                                                expires: DateTime.Now.AddMinutes(600),
                                                signingCredentials: credentials);

                        new JwtSecurityTokenHandler().WriteToken(token);

                        return Task.FromResult(true);
                    }
                }

                throw new EntityDoesntExistException();

            }
        }
    }
}
