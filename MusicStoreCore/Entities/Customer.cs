using MusicStoreCore.Enums;

namespace MusicStoreCore.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty!;
        public string LastName { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
        public Role Role { get; set; }
        public Status Status { get; set; }
        public DateTime StatusExpirationDate { get; set; }
        public double MoneySpent { get; set; }
        public IList<Order> Orders { get; set; } = new List<Order>();
    }
}
