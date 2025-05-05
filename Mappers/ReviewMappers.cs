using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Review;
using BookStore.Models;
using BookStore.Models.Entities;

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
                Images = review.Images.Select(i => i.ToReviewImageDto()).ToList(),
                LikeNumber = review.LikeNumber
            };
        }

        public static Review ToReviewModel(this CreateReviewDto createReviewDto, Guid userId) {
            return new Review {
                Content = createReviewDto.Content,
                Rating = createReviewDto.Rating,
                BookId = createReviewDto.BookId,
                UserId = userId,
            };
        }

        public static ReviewReply ToReviewReplyModel(this CreateReviewReplyDto createReviewReplyDto, Guid userId) {
            return new ReviewReply {
                ReviewId = createReviewReplyDto.ReviewId,
                UserId = userId,
                Content = createReviewReplyDto.Content
            };
        }

        public static ReviewReplyDto ToReviewReplyDto(this ReviewReply reviewReply) {
            return new ReviewReplyDto {
                Id = reviewReply.Id,
                ReviewId = reviewReply.Id,
                Content = reviewReply.Content,
                User = reviewReply.User.ToUserReviewDto()
            };
        }
    }
}