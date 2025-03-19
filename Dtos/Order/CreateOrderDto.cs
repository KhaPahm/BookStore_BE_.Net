using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Order
{
    public class CreateOrderDto
    {
        public string? UserNote { get; set; }
        public string PaymentMethod { get; set; } = "Cash";
        public string? PayPalTransactionId { get; set; } 
        public List<CreateOrderDetailDto> OrderDetailDtos { get; set; } = new();
    }
}