using MusicStoreCore.Enums;

namespace MusicStoreCore.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Grade Grade { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
