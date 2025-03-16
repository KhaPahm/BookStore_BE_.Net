using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.UserAddress;
using BookStore.Extensions;
using BookStore.Interfaces;
using BookStore.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/userAddress")]
    [ApiController]
    public class UserAddressController:ControllerBase
    {
        private readonly IUserAddressRepository _userAddressRepo;

        public UserAddressController(IUserAddressRepository userAddressRepo)
        {
            _userAddressRepo = userAddressRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByUserId() {
            var userId = User.GetUserId();
            var userAddress = await _userAddressRepo.GetAllByUserIdAsync(userId);

            var userAddressDtoList = userAddress.Select(ud => ud.ToUserAddressDto()).ToList();
            return Ok(userAddressDtoList);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserAddressDto userAddressDto) {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var userAddress = userAddressDto.ToUserAddressFromCreateDto();
            userAddress.UserId = userId;
            var newUserAddress = await _userAddressRepo.CreateAsync(userAddress);

            return Ok(newUserAddress.ToUserAddressDto());
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateUserAddressDto userAddressDto) {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var userAddress = userAddressDto.ToUserAddressFromCreateDto();

            var userAddressModel = await _userAddressRepo.UpdateAsync(id, userAddress);
            if(userAddressModel == null) 
                return NotFound();

            var udModel = userAddressModel.ToUserAddressDto();
            return Ok(udModel);
        }
    }
}