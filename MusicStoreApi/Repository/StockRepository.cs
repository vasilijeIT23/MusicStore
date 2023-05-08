using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class StockRepository : Repository<Stock>
    {
        public StockRepository(MusicStoreContext context) : base(context) { }

    }
}
