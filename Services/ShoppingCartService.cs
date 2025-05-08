using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ShoppingCart;
using BookStore.Exceptions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;

namespace BookStore.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IBookRepository _bookRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IBookRepository bookRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _bookRepository = bookRepository;
        }

        public async Task<List<ShoppingCartDto>> GetAllByUserIdAsync(Guid userId)
        {
            var shoppingCarts = await _shoppingCartRepository.GetAllByUserIdAsync(userId);
            var shoppingCartDtos = shoppingCarts.Select(sc => sc.ToShoppingCartDto()).ToList();
            return shoppingCartDtos;
        }

        public async Task<ShoppingCartDto> AddAsync(AddShoppingCartDto addShoppingCartDto, Guid userId)
        {
            var shoppingCartModel = addShoppingCartDto.ToShoppingCart(userId);
            var isBookExists = await _bookRepository.IsBookExistAsync(shoppingCartModel.BookId);
            if (!isBookExists)
            {
                throw new NotFoundException("Book not found");
            }

            await _shoppingCartRepository.CreateAsync(shoppingCartModel);
            return shoppingCartModel.ToShoppingCartDto();
        }

        public async Task<ShoppingCartDto> UpdateAsync(UpdateShoppingCartDto updateShoppingCartDto, Guid userId)
        {
            var shoppingCartModel = updateShoppingCartDto.ToShoppingCart(userId);
            await _shoppingCartRepository.UpdateAsync(shoppingCartModel);
            return shoppingCartModel.ToShoppingCartDto();
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid bookId)
        {
            var shoppingCart = await _shoppingCartRepository.GetByIdAsync(userId, bookId);
            if (shoppingCart == null)
            {
                return false;
            }

            await _shoppingCartRepository.DeleteAsync(shoppingCart);
            return true;
        }
    }
}