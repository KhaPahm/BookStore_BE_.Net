using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Order;
using BookStore.Models;

namespace BookStore.Mappers
{
    public static class OrderDetailMappers
    {
        public static OrderDetail ToOderDetailFromDto(this CreateOrderDetailDto orderDetailDto, Guid orderId) {
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
    }
}