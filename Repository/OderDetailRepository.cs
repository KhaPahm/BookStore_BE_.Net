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
    public class OderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDBContext _context;

        public OderDetailRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> CreateAsync(List<OrderDetail> orderDetails)
        {
            //Vòng lặp để kiểm tra id book - (chưa xử lý khi không tìm thấy sách)
            foreach(var orderDetail in orderDetails) {
                var book = await _context.Books.FirstAsync(b => b.Id == orderDetail.BookId);
                orderDetail.PriceAtPurchase = book.Price;
                book.StockQuantity -= orderDetail.Quantity;
                await _context.OrderDetails.AddAsync(orderDetail);
                await _context.SaveChangesAsync();
            }

            return orderDetails;
        }

        public async Task<OrderDetail> CreateAsync(OrderDetail orderDetail)
        {
            var book = await _context.Books.FirstAsync(b => b.Id == orderDetail.BookId);
            orderDetail.PriceAtPurchase = book.Price;
            book.StockQuantity -= orderDetail.Quantity;
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<List<OrderDetail>> GetByOrderIdAsync(Guid orderId)
        {
            var orderDetails = await _context.OrderDetails
                            .Include(od => od.Book)
                                .ThenInclude(b => b.Category)
                            .Include(od => od.Book)
                                .ThenInclude(b => b.Publisher)
                            .Include(od => od.Book)
                                .ThenInclude(b => b.Images)
                            .Where(od => od.OrderId == orderId).ToListAsync();
            return orderDetails;
        }
    }
}