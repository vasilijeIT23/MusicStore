using Microsoft.EntityFrameworkCore;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;
using System.Linq.Expressions;

namespace MusicStoreApi.Repository
{
    public class CartRepository : Repository<Cart>
    {
        public CartRepository(MusicStoreContext context) : base(context) { }

        public override IEnumerable<Cart> GetAll() => dbSet.Include(x => x.CartItems);
        public override Cart? GetById(Guid id) => dbSet.Include(x => x.CartItems).FirstOrDefault(x => x.Id == id);
        public override IEnumerable<Cart> Find(Expression<Func<Cart, bool>> predicate) => dbSet.Include(x => x. CartItems).Where(predicate);
    }
}
