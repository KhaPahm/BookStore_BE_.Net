using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.UserAddress;

namespace BookStore.Interfaces.Services
{
    public interface IUserAddressService
    {
        public Task<List<UserAddressDto>> GetByUserIdAsync(Guid userId);
        public Task<UserAddressDto> CreateAsync(CreateUserAddressDto userAddressDto, Guid userId);
        public Task<UserAddressDto> UpdateAsync(Guid userAddressId, CreateUserAddressDto userAddressDto);
        public Task<bool> DeleteAsync(Guid userAddressId);
    }
}