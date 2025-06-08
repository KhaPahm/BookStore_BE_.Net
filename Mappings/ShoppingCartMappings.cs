using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ShoppingCart;
using BookStore.Models;

namespace BookStore.Mappings
{
    public static class ShoppingCartMappings
    {
        public static ShoppingCartDto ToShoppingCartDto(this ShoppingCart shoppingCart) {
            return new ShoppingCartDto {
                Quantity = shoppingCart.Quantity,
                Book = shoppingCart.Book.ToBookDto()
            };
        }

        public static ShoppingCart ToShoppingCartModel(this AddShoppingCartDto shoppingCartDto, Guid userId) {
            return new ShoppingCart {
                BookId = shoppingCartDto.bookId,
                Quantity = shoppingCartDto.Quantity,
                UserId = userId,
            };
        }

        public static ShoppingCart ToShoppingCartModel(this UpdateShoppingCartDto shoppingCartDto, Guid userId) {
            return new ShoppingCart {
                BookId = shoppingCartDto.bookId,
                Quantity = shoppingCartDto.Quantity,
                UserId = userId,
            };
        }
    }
}