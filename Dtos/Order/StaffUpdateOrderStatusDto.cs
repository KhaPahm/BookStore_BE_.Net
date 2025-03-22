using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Order
{
    public class StaffUpdateOrderStatusDto
    {
        public string Status { get; set; }
        public string SystemNote { get; set; }
    }
}