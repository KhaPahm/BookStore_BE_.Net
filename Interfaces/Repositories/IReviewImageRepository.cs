using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IReviewImageRepository
    {
        public Task<ReviewImage> CreateAsync(ReviewImage reviewImage);
    }
}