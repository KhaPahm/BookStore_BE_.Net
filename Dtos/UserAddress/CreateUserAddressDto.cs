using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.UserAddress
{
    public class CreateUserAddressDto
    {
        public string Address { get; set; }
        public string Type { get; set; } = "HOME"; //OFFICE
        public bool IsDefault { get; set; } = false;
    }
}