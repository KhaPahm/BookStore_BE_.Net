using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Order
{
    public class CreateOrderDetailDto
    {
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}