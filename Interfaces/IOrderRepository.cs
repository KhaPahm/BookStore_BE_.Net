using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetAsync(Guid userId);
        public Task<Order> GetByIdAsync(Guid userId, Guid orderId);
        public Task<Order> CreateAysnc(Order order);
        public Task<Order> UpdateTotalPriceAsync(Guid orderId, double totalPrice);
    }
}