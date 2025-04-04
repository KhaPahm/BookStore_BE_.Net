using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class UserAddress
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Type { get; set; } = "HOME"; //OFFICE
        public bool IsDefault { get; set; } = false;
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}