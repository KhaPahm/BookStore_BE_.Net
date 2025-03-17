using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class OrderDetail
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public Order Order { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public double PriceAtPurchase { get; set; }
    }
}