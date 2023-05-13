namespace MusicStoreCore.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public IList<OrderItem> OrderItems { get; set; }

        public Order() { }  

        public Order(Customer customer, double price)
        {
            Customer = customer;
            OrderDate = DateTime.Now;
            Price = price;
            OrderItems = new List<OrderItem>();
        }
    }
}
