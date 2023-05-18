using Microsoft.EntityFrameworkCore;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class StockRepository : Repository<Stock>
    {
        public StockRepository(MusicStoreContext context) : base(context) { }

        public override IEnumerable<Stock> GetAll() => dbSet.Include(x => x.Warehouse).Include(x => x.Product);
        public override Stock? GetById(Guid id) => dbSet.Include(x => x.Warehouse).Include(x => x.Product).FirstOrDefault(x => x.Id == id);
    }
}
