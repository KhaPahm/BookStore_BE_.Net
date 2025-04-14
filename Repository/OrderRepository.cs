using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Order;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order> UserCancelOrderAsync(Guid orderId, string cancelReason)
        {
            var order = await _context.Orders.FirstAsync(o => o.Id == orderId);

            order.Status = "CANCELING";
            order.CancelReason = cancelReason;

            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> CreateAysnc(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        
        public async Task<List<Order>> GetAsync(Guid userId)
        {
            var orders = await _context.Orders
                                .Where(o => o.UserId == userId).ToListAsync();
            return orders;
        }

        public Order GetById(Guid userId, Guid orderId)
        {
            var order = _context.Orders
                                .FirstOrDefault(o => o.UserId == userId && o.Id == orderId);

            return order;
        }

        public async Task<Order?> GetByIdAsync(Guid userId, Guid orderId)
        {
            var order = await _context.Orders
                                .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId)
                                .ConfigureAwait(true);
            return order;
        }

        public async Task<Order> UpdateShippingAddress(Guid orderId, string address)
        {
            var order = await _context.Orders.FirstAsync(o => o.Id == orderId);
            order.ShippingAddress = address;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateTotalPriceAsync(Guid orderId, double totalPrice)
        {
            var order = await _context.Orders.FirstAsync(O => O.Id == orderId);
            order.TotalPrice = totalPrice;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderStatusAsycn(Guid orderId, StaffUpdateOrderStatusDto orderStatusDto)
        {
            var order = await _context.Orders.FirstAsync(O => O.Id == orderId);
            order.Status = orderStatusDto.Status;
            order.SystemNote = orderStatusDto.SystemNote;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task UpdatePayPalOrderId(Guid orderId, string paypalTransactionId)
        {
            var order = await _context.Orders.FirstAsync(o => o.Id == orderId);
            order.PayPalTransactionId = paypalTransactionId;
            await _context.SaveChangesAsync();
        }

        public async Task<Order> UpdateOrderStatusByTransactionIdAsycn(string transactionId, string orderStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.PayPalTransactionId == transactionId);
            if(order == null) 
                return null;

            order.Status = orderStatus;
            await _context.SaveChangesAsync();
            return order;
        }
    }
}