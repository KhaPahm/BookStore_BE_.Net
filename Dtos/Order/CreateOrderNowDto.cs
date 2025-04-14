using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Static;

namespace BookStore.Dtos.Order
{
    public class CreateOrderNowDto
    {
        public string? UserNote { get; set; }
        public string PaymentMethod { get; set; } = PaymentMethods.Cash;
        public string ShippingAddress { get; set; } = "";
        public CreateOrderDetailDto OrderDetail { get; set; }
    }
}