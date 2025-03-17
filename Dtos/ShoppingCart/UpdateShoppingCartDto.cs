using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.ShoppingCart
{
    public class UpdateShoppingCartDto
    {
        public Guid bookId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}