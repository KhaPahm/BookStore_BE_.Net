using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ReviewImage;
using BookStore.Models;

namespace BookStore.Mappings
{
    public static class ReviewImageMappings
    {
        public static ReviewImageDto ToReviewImageDto(this ReviewImage reviewImage) {
            return new ReviewImageDto {
                Id = reviewImage.Id,
                ImageUrl = reviewImage.ImageUrl
            };
        }
    }
}