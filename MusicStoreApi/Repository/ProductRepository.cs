using Microsoft.EntityFrameworkCore;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(MusicStoreContext context) : base(context) { }

        public override IEnumerable<Product> GetAll() => dbSet.Include(x => x.ProductType);
        public override Product? GetById(Guid id) => dbSet.Include(x => x.ProductType).FirstOrDefault(x => x.Id == id);
    }

}
