using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ReviewImage;
using BookStore.Models;

namespace BookStore.Mappers
{
    public static class ReviewImageMappers
    {
        public static ReviewImageDto ToReviewImageDto(this ReviewImage reviewImage) {
            return new ReviewImageDto {
                Id = reviewImage.Id,
                ImageUrl = reviewImage.ImageUrl
            };
        }
    }
}