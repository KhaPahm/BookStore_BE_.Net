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
                Address = userAddress.Address
            };
        }

        public static UserAddress ToUserAddressFromCreateDto(this CreateUserAddressDto userAddressDto) {
            return new UserAddress {
                Address = userAddressDto.Address
            };
        }
    }
}