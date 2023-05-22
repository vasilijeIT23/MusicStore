namespace MusicStoreCore.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; } = null!;
        public double CartValue { get; set; }
        public List<CartItem> CartItems { get; set; }

        public Cart() { }

        public Cart(Customer customer)
        {
            Customer = customer;
            CartValue = 0.0d;
            CartItems= new List<CartItem>();
        }
    }
}
