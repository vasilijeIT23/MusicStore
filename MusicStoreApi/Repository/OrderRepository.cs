using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(MusicStoreContext context) : base(context) { }

    }
}
