using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Order;

namespace BookStore.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<List<OrderDto>> GetOrdersAsync(Guid userId);
        public Task<OrderDto> GetOrderByIdAsync(Guid userId, Guid orderId);
        public Task<string> CreateOrderNowAsync(Guid userId, CreateOrderNowDto orderDto);
        public Task<bool> ExcutePaymentAsync(string paymentToken);
    }
}