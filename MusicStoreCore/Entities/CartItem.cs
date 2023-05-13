using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreCore.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; } = null!;
        public Cart Cart { get; set; } = null!;
        public int Quantity { get; set; }

        public CartItem() { }

        public CartItem(Product product, Cart cart, int quantity)
        {
            Product = product;
            Cart = cart;
            Quantity = quantity;
        }
    }
}
