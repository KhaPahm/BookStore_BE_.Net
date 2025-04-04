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
    public class ReviewReplyRepository : IReviewReplyRepository
    {
        private readonly ApplicationDBContext _context;

        public ReviewReplyRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<ReviewReply> CreateAsync(ReviewReply reviewReply)
        {
            await _context.ReviewReplies.AddAsync(reviewReply);
            await _context.SaveChangesAsync();
            return reviewReply;
        }

        public async Task<ReviewReply?> GetByIdAsync(Guid id)
        {
            return await _context.ReviewReplies
            .Include(rv => rv.User)
            .FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<List<ReviewReply>> GetByReviewIdAsync(Guid reviewId)
        {
            var reviewReplies = _context.ReviewReplies
            .Include(rv => rv.User)
            .Where(rv => rv.ReviewId == reviewId)
            .ToListAsync();

            return reviewReplies;
        }
    }
}