using Microsoft.EntityFrameworkCore;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;
using System.Linq.Expressions;

namespace MusicStoreApi.Repository
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(MusicStoreContext context) : base(context) { }

        public override IEnumerable<Order> GetAll() => dbSet.Include(x => x.OrderItems).ThenInclude(x => x.Product).Include(x => x.Customer);
        public override Order? GetById(Guid id) => dbSet.Include(x => x.Customer).Include(x => x.OrderItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);
        public override IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate) => dbSet.Include(x => x.Customer).Include(x => x.OrderItems).ThenInclude(x => x.Product).Where(predicate);
    }
}
