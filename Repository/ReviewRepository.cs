using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Review;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDBContext _context;

        public ReviewRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Review> CreateAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            return review;
        }

        public async Task<List<Review>> GetByBookIdAsync(Guid bookId)
        {
            return await _context.Reviews
                .Include(rv => rv.Images)
                .Include(rv => rv.User)
                .Where(rv => rv.BookId == bookId)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(Guid reviewId)
        {
            return await _context.Reviews
                .Include(rv => rv.Images)
                .Include(rv => rv.User)
                .FirstOrDefaultAsync(rv => rv.Id == reviewId);
        }

        public async Task<Review> UpdateAsync(Guid reviewId, UpdateReviewDto updateReviewDto)
        {
            var rv = await _context.Reviews
                .FirstAsync(rv => rv.Id == reviewId);

            rv.Content = updateReviewDto.Content;
            rv.Rating = updateReviewDto.Rating;

            await _context.SaveChangesAsync();

            return rv;
        }
    }
}