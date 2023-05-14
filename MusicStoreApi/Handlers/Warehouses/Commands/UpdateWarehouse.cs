using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;
using MusicStoreApi.Exceptions;

namespace MusicStoreApi.Handlers.Warehouses.Commands
{
    public static class UpdateWarehouse
    {
        [PublicAPI]
        public class Command : IRequest<Warehouse>
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Capacity { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Warehouse>();
            }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Warehouse>
        {
            private readonly IRepository<Warehouse> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<Warehouse> repository, IMapper mapper)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public Task<Warehouse> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var warehouse = _repository.GetById(request.Id);

                if (warehouse == null)
                {
                    throw new EntityDoesntExistException();
                }

                _mapper.Map(request, warehouse);
                _repository.SaveChanges();

                return Task.FromResult(warehouse);
            }
        }
    }
}
