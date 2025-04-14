using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Static;

namespace BookStore.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; } = OrderStatus.Created; //preparing--SHIPPING--COMPLETED--CANCELING--CANCELED
        public string? UserNote { get; set; }
        public string? CancelReason { get; set; }
        public string? SystemNote { get; set; }
        public string PaymentMethod { get; set; } = PaymentMethods.Cash; //Cash-Paypal
        public string? PayPalTransactionId { get; set; } 
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string? ShippingAddress { get; set; } = "";
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}