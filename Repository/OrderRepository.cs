using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
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

        public async Task<Order> CreateAysnc(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAsync(Guid userId)
        {
            var orders = await _context.Orders
                                .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Book)
                                        .ThenInclude(od => od.Category)
                                .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Book)
                                        .ThenInclude(od => od.Publisher)
                                .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Book)
                                        .ThenInclude(od => od.Images)
                                .Where(o => o.UserId == userId).ToListAsync();
            return orders;
        }

        public async Task<Order?> GetByIdAsync(Guid userId, Guid orderId)
        {
            var order = await _context.Orders
                                .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Book)
                                        .ThenInclude(od => od.Category)
                                .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Book)
                                        .ThenInclude(od => od.Publisher)
                                .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Book)
                                        .ThenInclude(od => od.Images)
                                .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId);
            return order;
        }

        public async Task<Order> UpdateTotalPriceAsync(Guid orderId, double totalPrice)
        {
            var order = await _context.Orders.FirstAsync(O => O.Id == orderId);
                            
            order.TotalPrice = totalPrice;
            await _context.SaveChangesAsync();

            return order;
        }
    }
}