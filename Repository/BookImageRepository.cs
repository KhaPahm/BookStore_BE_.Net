using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;

namespace BookStore.Repository
{
    public class BookImageRepository : IBookImageRepository
    {
        private readonly ApplicationDBContext _context;

        public BookImageRepository(ApplicationDBContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
        }

        public async Task<List<BookImage>> CreateAsync(ICollection<BookImage> bookImages)
        {
            List<BookImage> lstBookImage = new();
            foreach(var bookImage in bookImages) {
                await _context.BookImages.AddAsync(bookImage);
                _context.SaveChanges();
                lstBookImage.Add(bookImage);
            } 

            return lstBookImage;
        }

        public async Task<BookImage> CreateAsync(BookImage bookImage)
        {
            await _context.BookImages.AddAsync(bookImage);
            await _context.SaveChangesAsync();
            return bookImage;
        }
    }
}