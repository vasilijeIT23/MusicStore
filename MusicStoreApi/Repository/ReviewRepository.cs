using MusicStoreCore.Entities;
using MusicStoreInfrastructure;

namespace MusicStoreApi.Repository
{
    public class ReviewRepository : Repository<Review>
    {
        public ReviewRepository(MusicStoreContext context) : base(context) { }
    }
}
