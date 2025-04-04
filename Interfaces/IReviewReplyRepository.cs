using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Entities;

namespace BookStore.Interfaces
{
    public interface IReviewReplyRepository
    {
        public Task<ReviewReply> GetByIdAsync(Guid id);
        public Task<ReviewReply> CreateAsync(ReviewReply reviewReply);
        public Task<List<ReviewReply>> GetByReviewIdAsync(Guid reviewId);
    }
}