using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; } = "Repair"; //Success-Repair-Shipping-Cancle
        public string? UserNote { get; set; }
        public string? SystemNote { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; //Cash-Paypal
        public string? PayPalTransactionId { get; set; } 
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }
}