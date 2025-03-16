using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.UserAddress
{
    public class UserAddressDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
    }
}