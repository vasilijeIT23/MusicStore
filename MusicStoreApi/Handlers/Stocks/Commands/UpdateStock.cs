using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Stocks.Commands
{
    public static class UpdateStock
    {
        [PublicAPI]
        public class Command : IRequest<Stock>
        {
            public Guid Id { get; set; }
            public int Quantity { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Stock>();
            }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Stock>
        {
            private readonly IRepository<Stock> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<Stock> repository, IMapper mapper)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public Task<Stock> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var stock = _repository.GetById(request.Id);

                if (stock == null)
                {
                    throw new EntityDoesntExistException();
                }

                _mapper.Map(request, stock);
                _repository.SaveChanges();

                return Task.FromResult(stock);
            }
        }
    }
}
