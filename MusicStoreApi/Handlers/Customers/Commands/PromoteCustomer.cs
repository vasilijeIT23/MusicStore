using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using MusicStoreCore.Enums;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public static class PromoteCustomer
    {
        [PublicAPI]
        public class Command : IRequest<Customer>
        {
            public Guid Id { get; set; }   
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Customer>
        {
            private readonly IRepository<Customer> _repository;

            public RequestHandler(IRepository<Customer> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }
            public Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
                var customer = _repository.GetById(request.Id);

                if (customer == null)
                {
                    throw new EntityDoesntExistException();
                }

                if(customer.Status == Status.Advanced || customer.MoneySpent <= 1000)
                {
                    throw new RequirementsNotSatisfiedException();
                }

                if(customer.Orders.Count(x => x.OrderDate > DateTime.Now.AddDays(-365)) < 4) 
                {
                    throw new RequirementsNotSatisfiedException();
                }

                customer.Status = Status.Advanced;
                _repository.SaveChanges();

                return Task.FromResult(customer);
            }
        }
    }
}
