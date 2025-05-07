using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.UserAddress;
using BookStore.Extensions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;
using BookStore.Models.ResponeApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/userAddress")]
    [ApiController]
    public class UserAddressController:ControllerBase
    {
        private readonly IUserAddressService _userAddressService;

        public UserAddressController(IUserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByUserId() {
            var userId = User.GetUserId();
            var userAddressDtoList = await _userAddressService.GetByUserIdAsync(userId);
            return Ok(new ApiResponse<List<UserAddressDto>>(200, userAddressDtoList));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserAddressDto userAddressDto) {

            var userId = User.GetUserId();
            var newUserAddessDto = await _userAddressService.CreateAsync(userAddressDto, userId);
            return Ok(new ApiResponse<UserAddressDto>(201, newUserAddessDto));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateUserAddressDto createUserAddressDto) {
            var userAddressDto = await _userAddressService.UpdateAsync(id, createUserAddressDto);
            return Ok(new ApiResponse<UserAddressDto>(200, userAddressDto));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            await _userAddressService.DeleteAsync(id);
            return Ok(new ApiResponse<string>(204, null));
        }
    }
}