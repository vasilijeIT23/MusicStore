using Microsoft.EntityFrameworkCore;
using MusicStoreCore.Entities;
using MusicStoreInfrastructure;
using System.Linq.Expressions;

namespace MusicStoreApi.Repository
{
    public class PaymentRepository : Repository<Payment>
    {
        public PaymentRepository(MusicStoreContext context) : base(context) { }


    }
}
