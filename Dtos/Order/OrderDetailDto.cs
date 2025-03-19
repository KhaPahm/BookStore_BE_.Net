using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Book;

namespace BookStore.Dtos.Order
{
    public class OrderDetailDto
    {
        public BookDto Book { get; set; }
        public int Quantity { get; set; }
        public double PriceAtPurchase { get; set; }
    }
}