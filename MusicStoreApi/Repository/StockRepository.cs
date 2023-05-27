using Microsoft.EntityFrameworkCore;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;
using System.Linq.Expressions;

namespace MusicStoreApi.Repository
{
    public class StockRepository : Repository<Stock>
    {
        public StockRepository(MusicStoreContext context) : base(context) { }

        public override IEnumerable<Stock> GetAll() => dbSet.Include(x => x.Product).Include(x => x.Warehouse);
        public override Stock? GetById(Guid id) => dbSet.Include(x => x.Warehouse).Include(x => x.Product).FirstOrDefault(x => x.Id == id);
        public override IEnumerable<Stock> Find(Expression<Func<Stock, bool>> predicate) => dbSet.Include(x => x.Warehouse).Include(x => x.Product).Where(predicate);

    }
}
