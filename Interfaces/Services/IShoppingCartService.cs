using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ShoppingCart;

namespace BookStore.Interfaces.Services
{
    public interface IShoppingCartService
    {
        public Task<List<ShoppingCartDto>> GetAllByUserIdAsync(Guid userId);
        public Task<ShoppingCartDto> AddAsync(AddShoppingCartDto addShoppingCartDto, Guid userId);
        public Task<ShoppingCartDto> UpdateAsync(UpdateShoppingCartDto updateShoppingCartDto, Guid userId);
        public Task<bool> DeleteAsync(Guid userId, Guid bookId);
    }
}