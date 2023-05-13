using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreCore.Entities
{
    public class Stock
    {
        public Guid Id { get; set; }
        public Product Product { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;
        public int Quantity { get; set; }


        public Stock() { }

        public Stock(Product product, Warehouse warehouse, int quantity)
        {
            Product = product;
            Warehouse = warehouse;
            Quantity = quantity;
        }
    }
}
