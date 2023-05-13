namespace MusicStoreCore.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; } = null!;
        public Order Order { get; set; } = null!;
        public int Quantity { get; set; }

        public OrderItem() { }

        public OrderItem(Product product, Order order, int quantity)
        {
            Product = product;
            Order = order;
            Quantity = quantity;
        }
    }
}
