using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class ReviewLikeRepository : IReviewLikeRepository
    {
        private readonly ApplicationDBContext _context;

        public ReviewLikeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ReviewLike> CheckLikeReviewAsync(Guid userId, Guid reviewId)
        {
            var reviewLike = await _context.ReviewLikes.FirstOrDefaultAsync(rvL => rvL.UserId == userId && rvL.ReviewId == reviewId);
            return reviewLike;
        }

        public async Task<ReviewLike> LikeReviewAsync(Guid userId, Guid reviewId)
        {
            var reviewLike = new ReviewLike() {
                UserId = userId,
                ReviewId = reviewId
            };

            await _context.ReviewLikes.AddAsync(reviewLike);
            await _context.SaveChangesAsync();
            return reviewLike;
        }
    }
}