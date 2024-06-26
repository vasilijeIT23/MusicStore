﻿using MusicStoreCore.Enums;
using System.Security.Cryptography;
using System.Text;
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
        public string Username { get; set; }= string.Empty!;
        public string Password { get; set; } = string.Empty!;
        public string Salt { get; set; } = string.Empty!;
        public IList<Order> Orders { get; set; }
        public IList<Review> Reviews { get; set; }

        public Customer() { }
        public Customer(string firstName, string lastName, string email, string username, string password, string salt)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Status = Status.Regular;
            Role = Role.Regular;
            StatusExpirationDate = null;
            MoneySpent = 0.0d;
            Username = username;
            Password = Hash(password) + Hash(salt);
            Salt = Hash(salt);
            Orders = new List<Order>();
            Reviews = new List<Review>();
        }

        public string Hash(string stringToHash)
        {
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(stringToHash)));
        }
    }
}
