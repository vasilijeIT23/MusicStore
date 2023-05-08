namespace MusicStoreCore.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
