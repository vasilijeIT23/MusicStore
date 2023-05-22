using Microsoft.EntityFrameworkCore;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;
using System.Linq.Expressions;

namespace MusicStoreApi.Repository
{
    public class OrderItemRepository : Repository<OrderItem>
    {
        public OrderItemRepository(MusicStoreContext context) : base(context) { }


        public override IEnumerable<OrderItem> GetAll() => dbSet.Include(x => x.Order).Include(x => x.Product);
        public override OrderItem? GetById(Guid id) => dbSet.Include(x => x.Order).Include(x => x.Product).FirstOrDefault(x => x.Id == id);
        public override IEnumerable<OrderItem> Find(Expression<Func<OrderItem, bool>> predicate) => dbSet.Include(x => x.Order).Include(x => x.Product).Include(x => x.Order).Where(predicate);
    }
}
