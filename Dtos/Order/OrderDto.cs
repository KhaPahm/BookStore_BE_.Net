using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; } = "Prepare"; //Success-Prepare-Shipping-Cancling-Cancled
        public string? UserNote { get; set; }
        public string? SystemNote { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; //Cash-Paypal
        public string? PayPalTransactionId { get; set; } 
        public DateTime? MyProperty { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}