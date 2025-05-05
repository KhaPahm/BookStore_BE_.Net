using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Review;

namespace BookStore.Interfaces.Services
{
    public interface IReviewService
    {
        public Task<List<ReviewDto>> GetByBookIdAsync(Guid bookId);
        public Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto, Guid userId);
        public Task<ReviewDto> UpdateReviewAsync(Guid reviewId, UpdateReviewDto updateReviewDto, Guid userId);
        public Task<bool> LikeReviewAsync(Guid reviewId, Guid userId);
        public Task<ReviewReplyDto> ReplyReviewAsync(Guid userId, CreateReviewReplyDto createReviewReplyDto);
        public Task<List<ReviewReplyDto>> GetReviewRepliesAsync(Guid reviewId);        
    }
}