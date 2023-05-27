using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreCore.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string PaymentId { get; set; } = string.Empty;
        public string CustomerId { get; set; }
        public long Price { get; set; }

        public Payment() { }

        public Payment(string paymentId, string customer, long price)
        {
            PaymentId = paymentId;
            CustomerId = customer;
            Price = price;
        }
    }
}
