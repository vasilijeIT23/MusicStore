namespace MusicStoreCore.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.Empty!;
        public string Name { get; set; } = string.Empty!;
        public bool InStock { get; set; }
        public double Price { get; set; }
        public ProductType ProductType { get; set; } = null!;
    }
}
