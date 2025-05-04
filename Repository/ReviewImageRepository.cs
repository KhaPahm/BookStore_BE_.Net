using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;

namespace BookStore.Repository
{
    public class ReviewImageRepository : IReviewImageRepository
    {
        private readonly ApplicationDBContext _context;

        public ReviewImageRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ReviewImage> CreateAsync(ReviewImage reviewImage)
        {
            await _context.ReviewImages.AddAsync(reviewImage);
            await _context.SaveChangesAsync();  
            return reviewImage;
        }
    }
}