namespace MusicStoreCore.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.Empty!;
        public string Name { get; set; } = string.Empty!;
        public bool InStock { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public ProductType ProductType { get; set; } = null!;
        public IList<Review> Reviews { get; set;} = new List<Review>();

        public Product(){}

        public Product( string name, double price, ProductType productType, string imagePath)
        {
            Name = name;
            InStock= false;
            Price = price;
            ProductType = productType;
            ImagePath = imagePath;
            Reviews = new List<Review>();
        }
    }
}
