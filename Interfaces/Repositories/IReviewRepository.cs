using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Review;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IReviewRepository
    {
        public Task<List<Review>> GetByBookIdAsync(Guid bookId);
        public Task<Review> GetByIdAsync(Guid reviewId);
        public Task<Review> CreateAsync(Review review);
        public Task<Review> UpdateAsync(Guid reviewId, UpdateReviewDto updateReviewDto);
        public Task<bool> IsExistAsync(Guid reviewId);
    }
}