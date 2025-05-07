using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.UserAddress;
using BookStore.Exceptions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;

namespace BookStore.Services
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IUserAddressRepository _userAddressRepository;

        public UserAddressService(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }
        
        public async Task<UserAddressDto> CreateAsync(CreateUserAddressDto userAddressDto, Guid userId)
        {
            var userAddress = userAddressDto.ToUserAddressModel();
            userAddress.UserId = userId;
            await _userAddressRepository.CreateAsync(userAddress);

            return userAddress.ToUserAddressDto();
        }

        public async Task<bool> DeleteAsync(Guid userAddressId)
        {
            var isExist = await _userAddressRepository.IsUserAddressExist(userAddressId);
            if (!isExist) 
                throw new NotFoundException("User address not found");

            await _userAddressRepository.DeleteAsync(userAddressId);
            return true;
        }

        public async Task<List<UserAddressDto>> GetByUserIdAsync(Guid userId)
        {
            var lstUserAddress = await _userAddressRepository.GetAllByUserIdAsync(userId);
            var userAddressDtoList = lstUserAddress.Select(ud => ud.ToUserAddressDto()).ToList();
            return userAddressDtoList;
        }

        public async Task<UserAddressDto> UpdateAsync(Guid userAddressId, CreateUserAddressDto userAddressDto)
        {
            var isExist = await _userAddressRepository.IsUserAddressExist(userAddressId);
            if (!isExist) 
                throw new NotFoundException("User address not found");

            var userAddress = userAddressDto.ToUserAddressModel();
            await _userAddressRepository.UpdateAsync(userAddressId, userAddress);
            
            return userAddress.ToUserAddressDto();
        }
    }
}