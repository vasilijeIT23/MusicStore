using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(MusicStoreContext context) : base(context) { }
    }

}
