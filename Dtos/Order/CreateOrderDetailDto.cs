using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Order
{
    public class CreateOrderDetailDto
    {
        public Guid BookId { get; set; }
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}