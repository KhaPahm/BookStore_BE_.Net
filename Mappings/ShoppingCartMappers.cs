using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ShoppingCart;
using BookStore.Models;

namespace BookStore.Mappers
{
    public static class ShoppingCartMappers
    {
        public static ShoppingCartDto ToShoppingCartDto(this ShoppingCart shoppingCart) {
            return new ShoppingCartDto {
                Quantity = shoppingCart.Quantity,
                Book = shoppingCart.Book.ToBookDto()
            };
        }

        public static ShoppingCart ToShoppingCart(this AddShoppingCartDto shoppingCartDto, Guid userId) {
            return new ShoppingCart {
                BookId = shoppingCartDto.bookId,
                Quantity = shoppingCartDto.Quantity,
                UserId = userId,
            };
        }

        public static ShoppingCart ToShoppingCart(this UpdateShoppingCartDto shoppingCartDto, Guid userId) {
            return new ShoppingCart {
                BookId = shoppingCartDto.bookId,
                Quantity = shoppingCartDto.Quantity,
                UserId = userId,
            };
        }
    }
}