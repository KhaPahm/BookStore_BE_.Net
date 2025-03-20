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
    public class UserAddressRepository : IUserAddressRepository
    {
        private readonly ApplicationDBContext _context;
        
        public UserAddressRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<UserAddress> CreateAsync(UserAddress userAddress)
        {
            await _context.UserAddresses.AddAsync(userAddress);
            await _context.SaveChangesAsync();
            return userAddress;
        }

        public async Task<UserAddress> DeleteAsync(Guid userAddressId)
        {
            var userAddress = await _context.UserAddresses.FirstOrDefaultAsync(ud => ud.Id == userAddressId);
            
            if(userAddress == null)
                return null;

            _context.UserAddresses.Remove(userAddress);
            await _context.SaveChangesAsync();

            return userAddress;
        }

        public async Task<List<UserAddress>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.UserAddresses.Where(ud => ud.UserId == userId).ToListAsync();
        }

        public async Task<UserAddress?> UpdateAsync(Guid userAddressId, UserAddress userAddress)
        {
            var userAddressModel = await _context.UserAddresses.FirstOrDefaultAsync(ud => ud.Id == userAddressId);
            if(userAddressModel == null)
                return null;

            userAddressModel.Address = userAddress.Address;
            userAddressModel.Type = userAddress.Type;
            userAddressModel.IsDefault = userAddress.IsDefault;
            await _context.SaveChangesAsync();

            return userAddressModel;
        }
    }
}