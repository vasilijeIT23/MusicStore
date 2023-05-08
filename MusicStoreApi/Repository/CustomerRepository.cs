using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(MusicStoreContext context) : base(context) { }

    }
}
