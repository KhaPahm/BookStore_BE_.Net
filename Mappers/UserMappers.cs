using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}