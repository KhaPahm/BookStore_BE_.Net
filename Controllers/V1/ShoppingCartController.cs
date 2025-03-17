using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ShoppingCart;
using BookStore.Extensions;
using BookStore.Interfaces;
using BookStore.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/shoppingCart")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepo;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepo)
        {
            _shoppingCartRepo = shoppingCartRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUserId() {
            var userId = User.GetUserId();
            var shoppingCarts = await _shoppingCartRepo.GetAllByUserIdAsync(userId);
            var shoppingCartDtos = shoppingCarts.Select(sc => sc.ToShoppingCartDto()).ToList();
            return Ok(shoppingCartDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddShoppingCartDto addShoppingCartDto) {
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var shoppingCartModel = addShoppingCartDto.ToShoppingCart(userId);

            var shoppingCart = await _shoppingCartRepo.CreateAsync(shoppingCartModel);

            if(shoppingCart == null) {
                return NotFound("Couldn't find book");
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateShoppingCartDto updateShoppingCartDto) {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.GetUserId();

            var shoppingCartModel = updateShoppingCartDto.ToShoppingCart(userId);

            var shoppingCart = await _shoppingCartRepo.UpdateAsync(shoppingCartModel);

            if(shoppingCart == null) {
                return NotFound("Couldn't find book");
            }
            return Ok();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid bookId) {
            var userId = User.GetUserId();

            var shoppingCart = await _shoppingCartRepo.DeleteAsync(userId, bookId);

            if(shoppingCart == null)
                return NotFound();

            return NoContent();
        }
    }
}