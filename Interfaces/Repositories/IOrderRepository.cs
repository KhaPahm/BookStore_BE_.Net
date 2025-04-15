using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Order;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetAsync(Guid userId);
        public Task<Order> GetByIdAsync(Guid userId, Guid orderId);
        public Task<Order> GetByIdAsync(Guid orderId);
        public Order GetById(Guid userId, Guid orderId);
        public Task<Order> CreateAysnc(Order order);
        public Task<Order> UpdateTotalPriceAsync(Guid orderId, double totalPrice);
        public Task UpdatePayPalOrderId(Guid orderId, string paypalTransactionId);
        public Task<Order> UpdateShippingAddress(Guid orderId, string address);
        public Task<Order> UserCancelOrderAsync(Guid orderId, string cancelReason);
        public Task<Order> UpdateOrderStatusAsycn(Guid orderId, StaffUpdateOrderStatusDto orderStatusDto);
        public Task<Order> UpdateOrderStatusByTransactionIdAsycn(string transactionId, string orderStatus);
    }
}