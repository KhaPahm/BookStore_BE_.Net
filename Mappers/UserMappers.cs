using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Auth;
using BookStore.Dtos.User;
using BookStore.Models;

namespace BookStore.Mappers
{
    public static class UserMappers
    {
        public static UserReviewDto ToUserReviewDto(this User user) {
            return new UserReviewDto {
                Id = user.Id,
                FullName = user.FullName,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }
 
        public static UserDto ToUserDto(this User user, string accessToken) {
            return new UserDto {
                FullName = user.FullName,
                Email = user.Email,
                ProfilePictureUrl = user.ProfilePictureUrl,
                AuthProvider = user.AuthProvider,
                Role = user.Role,
                Token = accessToken
            };
        }
    }
}