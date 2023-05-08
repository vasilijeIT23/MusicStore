using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class ProductTypeRepository : Repository<ProductType>
    {
        public ProductTypeRepository(MusicStoreContext context) : base(context) { }

    }
}
