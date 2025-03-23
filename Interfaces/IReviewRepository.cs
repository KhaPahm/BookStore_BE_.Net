using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IReviewRepository
    {
        public Task<List<Review>> GetByBookIdAsync(Guid bookId);
    }
}