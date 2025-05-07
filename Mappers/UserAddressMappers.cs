using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.UserAddress;
using BookStore.Models;

namespace BookStore.Mappers
{
    public static class UserAddressMappers
    {
        public static UserAddressDto ToUserAddressDto(this UserAddress userAddress) {
            return new UserAddressDto {
                Id = userAddress.Id,
                Address = userAddress.Address,
                Type = userAddress.Type,
                IsDefault = userAddress.IsDefault
            };
        }

        public static UserAddress ToUserAddressModel(this CreateUserAddressDto userAddressDto) {
            return new UserAddress {
                Address = userAddressDto.Address,
                Type = userAddressDto.Type,
                IsDefault = userAddressDto.IsDefault
            };
        }
    }
}