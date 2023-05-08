using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class CartItemRepository :Repository<CartItem>
    {
        public CartItemRepository(MusicStoreContext context) : base(context) { }

    }
}
