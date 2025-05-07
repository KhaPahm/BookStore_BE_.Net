using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IUserAddressRepository
    {
        public Task<List<UserAddress>> GetAllByUserIdAsync(Guid userId);
        public Task<UserAddress> CreateAsync(UserAddress userAddress);
        public Task<UserAddress> UpdateAsync(Guid userAddressId, UserAddress userAddress);
        public Task<UserAddress> DeleteAsync(Guid userAddressId);
        public Task<bool> IsUserAddressExist(Guid userAddressId); 
    }
}