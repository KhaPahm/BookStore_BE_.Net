using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Book;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _context;

        public BookRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
            .Include(book => book.Images)
            .Include(book => book.Category)
            .Include(book => book.Publisher)
            .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Books
            .Include(book => book.Images)
            .Include(book => book.Category)
            .Include(book => book.Publisher)
            .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> UpdateAsync(Guid id, Book book)
        {
            var bookModel = await _context.Books.Include(b => b.Category)
                                            .Include(b => b.Publisher)
                                            .Include(b => b.Images)
                                            .FirstOrDefaultAsync(b => b.Id == id);
            if(bookModel == null) 
                return null;

            bookModel.Title = book.Title;
            bookModel.Author = book.Author;
            bookModel.Description = book.Description;
            bookModel.Price = book.Price;
            bookModel.StockQuantity = book.StockQuantity;
            bookModel.PublishedDate = book.PublishedDate;
            bookModel.CreateAt = book.CreateAt;
            bookModel.CategoryId = book.CategoryId;
            bookModel.PublisherId = book.PublisherId;

            await _context.SaveChangesAsync();

            return bookModel;
        }
    }
}