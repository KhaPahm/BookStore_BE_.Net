using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Book;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAllAsync();
        public Task<Book> GetByIdAsync(Guid id);
        public Task<Book> CreateAsync(Book book);
        public Task<Book> UpdateAsync(Guid id, Book book);
        public Task<bool> IsBookExistAsync(Guid id);
    }
}