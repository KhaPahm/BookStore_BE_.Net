using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ShoppingCart;
using BookStore.Extensions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/shoppingCart")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService) 
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUserId() {
            var userId = User.GetUserId();
            var shoppingCartDtos = await _shoppingCartService.GetAllByUserIdAsync(userId);
            return Ok(shoppingCartDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddShoppingCartDto addShoppingCartDto) 
        {
            var userId = User.GetUserId();
            await _shoppingCartService.AddAsync(addShoppingCartDto, userId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateShoppingCartDto updateShoppingCartDto) 
        {
            var userId = User.GetUserId();
            await _shoppingCartService.UpdateAsync(updateShoppingCartDto, userId);
            return Ok();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid bookId) {
            var userId = User.GetUserId();
            await _shoppingCartService.DeleteAsync(userId, bookId);
            return NoContent();
        }
    }
}