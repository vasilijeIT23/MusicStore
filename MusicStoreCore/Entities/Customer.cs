using MusicStoreCore.Enums;
using System.Xml.Linq;

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
        public DateTime? StatusExpirationDate { get; set; }
        public double MoneySpent { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<Review> Reviews { get; set; }


        public Customer(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Status = Status.Regular;
            Role = Role.Regular;
            StatusExpirationDate = null;
            MoneySpent = 0;
            Orders = new List<Order>();
            Reviews = new List<Review>();
        }
    }
}
