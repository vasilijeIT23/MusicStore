namespace MusicStoreCore.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; } = null!;
        public double CartValue { get; set; }
        public IList<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
