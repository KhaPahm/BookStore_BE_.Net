using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Entities;

namespace BookStore.Interfaces
{
    public interface IReviewLikeRepository
    {
        public Task<ReviewLike> CheckLikeReviewAsync(Guid userId, Guid reviewId);
        public Task<ReviewLike> LikeReviewAsync(Guid userId, Guid reviewId);
    }
}