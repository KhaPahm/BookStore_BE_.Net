using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IShoppingCartRepository
    {
        public Task<List<ShoppingCart>> GetAllByUserIdAsync(Guid userId);
        public Task<ShoppingCart?> GetByIdAsync(Guid userId, Guid bookId);
        public Task<ShoppingCart> CreateAsync(ShoppingCart shoppingCart);
        public Task<ShoppingCart> UpdateAsync(ShoppingCart shoppingCart);
        public Task<ShoppingCart> DeleteAsync(ShoppingCart shoppingCart);
        public Task ClearAsync(Guid userId);
    }
}