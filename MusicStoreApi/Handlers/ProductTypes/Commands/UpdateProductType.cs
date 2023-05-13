using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.ProductTypes.Commands
{
    public static class UpdateProductType
    {
        [PublicAPI]
        public class Command : IRequest<ProductType>
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
                CreateMap<Command, ProductType>();
            }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, ProductType>
        {
            private readonly IRepository<ProductType> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<ProductType> repository, IMapper mapper)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public Task<ProductType> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var productType = _repository.GetById(request.Id);

                if (productType == null)
                {
                    throw new EntityDoesntExistException(request);
                }

                _mapper.Map(request, productType);
                _repository.SaveChanges();

                return Task.FromResult(productType);
            }
        }
    }
}
