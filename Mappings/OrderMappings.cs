using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Order;
using BookStore.Models;

namespace BookStore.Mappings
{
    public static class OrderMappings
    {
        public static Order ToOrderModel(this CreateOrderDto orderDto, Guid userId) {
            return new Order {
                UserNote = orderDto.UserNote,
                PaymentMethod = orderDto.PaymentMethod,
                PayPalTransactionId = orderDto.PayPalTransactionId,
                ShippingAddress = orderDto.ShippingAddress,
                UserId = userId
            };
        }

        public static OrderDto ToOrderDto(this Order order, List<OrderDetail> orderDetails) {
            return new OrderDto {
                Id = order.Id,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                UserNote = order.UserNote,
                CancelReason = order.CancelReason,
                SystemNote = order.SystemNote,
                PaymentMethod = order.PaymentMethod,
                PayPalTransactionId = order.PayPalTransactionId,
                ShippingAddress = order.ShippingAddress,
                OrderDetails = orderDetails.Select(od => od.ToOrderDetailDto()).ToList()
            };
        }

        public static Order ToOrderModel(this CreateOrderNowDto orderNowDto, Guid userId) {
            return new Order {
                UserNote = orderNowDto.UserNote,
                PaymentMethod = orderNowDto.PaymentMethod,
                // PayPalTransactionId = orderNowDto.PayPalTransactionId,
                ShippingAddress = orderNowDto.ShippingAddress,
                UserId = userId
            };
        }
    }
}