using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Order;
using BookStore.Models;

namespace BookStore.Mappings
{
    public static class OrderDetailMappings
    {
        public static OrderDetail ToOrderDetailModel(this CreateOrderDetailDto orderDetailDto, Guid orderId) {
            return new OrderDetail {
                BookId = orderDetailDto.BookId,
                OrderId = orderId,
                Quantity = orderDetailDto.Quantity
            };
        } 

        public static OrderDetailDto ToOrderDetailDto(this OrderDetail orderDetail) {
            return new OrderDetailDto {
                Book = orderDetail.Book.ToBookDto(),
                Quantity = orderDetail.Quantity,
                PriceAtPurchase = orderDetail.PriceAtPurchase
            };
        }

        public static OrderDetail ToOrderDetailModel(this ShoppingCart shoppingCart, Guid orderId) {
            return new OrderDetail {
                OrderId = orderId,
                BookId = shoppingCart.BookId,
                Quantity = shoppingCart.Quantity,
                PriceAtPurchase = shoppingCart.Book.Price
            };
        }
    }
}