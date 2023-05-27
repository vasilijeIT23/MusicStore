using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class WarehouseRepository : Repository<Warehouse>
    {
        public WarehouseRepository(MusicStoreContext context) : base(context) { }



    }
}
