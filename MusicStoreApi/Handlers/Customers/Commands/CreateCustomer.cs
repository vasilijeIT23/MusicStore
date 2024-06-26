﻿using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using System.Security.Cryptography;

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
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Customer>
        {
            private readonly IRepository<Customer> _customerRepository;
            private readonly IRepository<Cart> _cartRepository;

            public RequestHandler(IRepository<Customer> customerRepository, IRepository<Cart> cartRepository)
            {
                _customerRepository = customerRepository;
                _cartRepository = cartRepository;
            }
            public Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                if (request.FirstName == "" ||
                    request.LastName == "" || request.Password.Length < 8 ||
                    request.Email.Contains('@') || request.Email.Contains(".com") || request.Username.Length < 8) 
                {
                    throw new InvalidInputValueException();
                }

                var existingCustomer = _customerRepository.Find(x => x.Email == request.Email && x.Username == request.Username).SingleOrDefault();

                if (existingCustomer != null)
                {
                    return Task.FromResult(existingCustomer);
                }

                var salt = GetSalt().ToString();
                var customer = new Customer(request.FirstName, request.LastName, request.Email, request.Username, request.Password, salt);

                _customerRepository.Create(customer);
                _customerRepository.SaveChanges();

                var cart = new Cart(customer);

                _cartRepository.Create(cart);
                _cartRepository.SaveChanges();

                return Task.FromResult(customer);
            }
        }

        private static int saltLengthLimit = 32;
        private static byte[] GetSalt()
        {
            return GetSalt(saltLengthLimit);
        }
        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
    }
}
