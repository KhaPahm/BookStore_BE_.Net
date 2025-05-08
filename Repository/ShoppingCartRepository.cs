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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDBContext _context;

        public ShoppingCartRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task ClearAsync(Guid userId)
        {
            var shoppingCarts = await _context.ShoppingCarts.Where(sc => sc.UserId == userId).ToListAsync();
            foreach(var sc in shoppingCarts) {
                _context.ShoppingCarts.Remove(sc);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ShoppingCart?> CreateAsync(ShoppingCart shoppingCart)
        {
            var shoppingCartCheck = await _context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.UserId == shoppingCart.UserId && sc.BookId == shoppingCart.BookId);

            if(shoppingCartCheck == null) {
                await _context.ShoppingCarts.AddAsync(shoppingCart);
            }
            else {
                shoppingCartCheck.Quantity += shoppingCart.Quantity;
            }

            await _context.SaveChangesAsync();
            return shoppingCart;
        }

        public async Task<ShoppingCart> DeleteAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
            await _context.SaveChangesAsync();
            return shoppingCart;
        }

        public async Task<List<ShoppingCart>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.ShoppingCarts
                        .Include(sc => sc.Book)
                            .ThenInclude(b => b.Category)
                        .Include(sc => sc.Book)
                            .ThenInclude(b => b.Publisher)
                        .Include(sc => sc.Book)
                            .ThenInclude(b => b.Images)
                        .Where(sc => sc.UserId == userId).ToListAsync();
        }

        public async Task<ShoppingCart?> GetByIdAsync(Guid userId, Guid bookId)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.UserId == userId && sc.BookId == bookId);
        }

        public async Task<ShoppingCart> UpdateAsync(ShoppingCart shoppingCart)
        {
            var shoppingCartCheck = await _context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.UserId == shoppingCart.UserId && sc.BookId == shoppingCart.BookId);

            if(shoppingCartCheck == null) {
                return await CreateAsync(shoppingCart);
            }

            shoppingCartCheck.Quantity = shoppingCart.Quantity;
            await _context.SaveChangesAsync();
            return shoppingCartCheck;
        }
    }
}