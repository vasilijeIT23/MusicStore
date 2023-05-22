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

        public Order(Customer customer)
        {
            Customer = customer;
            OrderDate = DateTime.Now;
            Price = 0.0d;
            OrderItems = new List<OrderItem>();
        }
    }
}
