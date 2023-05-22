using Microsoft.EntityFrameworkCore;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;
using System.Linq.Expressions;

namespace MusicStoreApi.Repository
{
    public class CartItemRepository :Repository<CartItem>
    {
        public CartItemRepository(MusicStoreContext context) : base(context) { }

        public override IEnumerable<CartItem> GetAll() => dbSet.Include(x => x.Cart).Include(x => x.Product);
        public override CartItem? GetById(Guid id) => dbSet.Include(x => x.Cart).Include(x => x.Product).FirstOrDefault(x => x.Id == id);

        public override IEnumerable<CartItem> Find(Expression<Func<CartItem, bool>> predicate) => dbSet.Include(x => x.Cart).Include(x => x.Product).Where(predicate);


    }
}
