using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Order
{
    public class UpdateOrderStatusDto
    {
        public string CancelReason { get; set; }
    }
}