namespace MusicStoreCore.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public bool PaymentCompleted { get; set; }
        public Payment Payment { get; set; } = null;
        public IList<OrderItem> OrderItems { get; set; }

        public Order() { }  

        public Order(Customer customer)
        {
            Customer = customer;
            OrderDate = DateTime.Now;
            Price = 0.0d;
            PaymentCompleted = false;
            OrderItems = new List<OrderItem>();
            Payment = new Payment("payment", "customer", 0);
        }
    }
}
