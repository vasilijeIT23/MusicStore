using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class CartRepository : Repository<Cart>
    {
        public CartRepository(MusicStoreContext context) : base(context) { }

    }
}
