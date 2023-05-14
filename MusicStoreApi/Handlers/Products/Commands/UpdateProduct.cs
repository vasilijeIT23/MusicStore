using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using MusicStoreApi.Exceptions;
using MusicStoreApi.Repository;
using MusicStoreCore.Entities;

namespace MusicStoreApi.Handlers.Products.Commands
{
    public static class UpdateProduct
    {
        [PublicAPI]
        public class Command : IRequest<Product>
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public bool InStock { get; set; }
            public double Price { get; set; }
            public ProductType ProductType { get; set; } = null!;
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Product>();
            }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Product>
        {
            private readonly IRepository<Product> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<Product> repository, IMapper mapper)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public Task<Product> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    throw new InvalidInputValueException();
                }

                var product = _repository.GetById(request.Id);

                if (product == null)
                {
                    throw new EntityDoesntExistException();
                }

                _mapper.Map(request, product);
                _repository.SaveChanges();

                return Task.FromResult(product);
            }
        }
    }
}
