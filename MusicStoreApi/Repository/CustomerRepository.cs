using Microsoft.EntityFrameworkCore;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;
using System.Linq.Expressions;

namespace MusicStoreApi.Repository
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(MusicStoreContext context) : base(context) { }

        public override IEnumerable<Customer> GetAll() => dbSet.Include(x => x.Orders).ThenInclude(x => x.OrderItems);
        public override Customer? GetById(Guid id) => dbSet.Include(x => x.Orders).ThenInclude(x => x.OrderItems).FirstOrDefault(x => x.Id == id);
        public override IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate) => dbSet.Include(x => x.Orders).ThenInclude(x => x.OrderItems).Where(predicate);

    }
}
