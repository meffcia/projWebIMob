using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Shared.Models;

namespace OnlineShop.Shared.Auth
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Role { get; set; } = "Customer"; // Default role is "Customer

        public List<Order> Orders { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
