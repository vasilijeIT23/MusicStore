using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using MusicStoreCore.Enums;

namespace MusicStoreApi.Handlers.Customers.Commands
{
    public class UpdateCustomer
    {
        [PublicAPI]
        public class Command : IRequest<Customer>
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public Role Role { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Customer>();
            }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Customer>
        {
            private readonly IRepository<Customer> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<Customer> repository, IMapper mapper)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                //BadRequest
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var customer = _repository.GetById(request.Id);
                //CustomerDoesntExist
                if (customer == null)
                {
                    throw new EntityDoesntExistException();
                }

                _mapper.Map(request, customer);
                _repository.SaveChanges();

                return Task.FromResult(customer);
            }
        }
    }
}
