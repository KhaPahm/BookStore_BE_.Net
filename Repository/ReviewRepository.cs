using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
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

        public async Task<List<Review>> GetByBookIdAsync(Guid bookId)
        {
            return await _context.Reviews.Where(rv => rv.BookId == bookId).ToListAsync();
        }
    }
}