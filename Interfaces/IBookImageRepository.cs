using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IBookImageRepository
    {
        public Task<List<BookImage>> CreateAsync(Guid bookId, ICollection<IFormFile> images);
    }
}