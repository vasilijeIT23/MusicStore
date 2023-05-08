using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class OrderItemRepository : Repository<OrderItem>
    {
        public OrderItemRepository(MusicStoreContext context) : base(context) { }

    }
}
