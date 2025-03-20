using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Order;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IOrderDetailRepository
    {
        public Task<List<OrderDetail>> CreateAsync(List<OrderDetail> orderDetails);
        public Task<OrderDetail> CreateAsync(OrderDetail orderDetail);
    }
}