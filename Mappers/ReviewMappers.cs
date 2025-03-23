using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Review;
using BookStore.Models;

namespace BookStore.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDto ToReviewDto(this Review review) {
            return new ReviewDto {
                Id = review.Id,
                Content = review.Content,
                Rating = review.Rating,
                User = review.User.ToUserReviewDto(),
                Images = review.Images.Select(i => i.ToReviewImageDto()).ToList()
            };
        }
    }
}