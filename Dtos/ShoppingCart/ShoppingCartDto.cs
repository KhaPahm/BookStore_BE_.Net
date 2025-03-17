using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Book;

namespace BookStore.Dtos.ShoppingCart
{
    public class ShoppingCartDto
    {
        public int Quantity { get; set; }
        public BookDto Book { get; set; }
    }
}