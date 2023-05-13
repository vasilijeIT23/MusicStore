namespace MusicStoreCore.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.Empty!;
        public string Name { get; set; } = string.Empty!;
        public bool InStock { get; set; }
        public double Price { get; set; }
        public ProductType ProductType { get; set; } = null!;
        public IList<Review> Reviews { get; set;} = new List<Review>();

        public Product()
        {
        }
        public Product( string name, double price, ProductType productType)
        {
            Name = name;
            InStock= false;
            Price = price;
            ProductType = productType;
            Reviews = new List<Review>();
        }
    }
}
