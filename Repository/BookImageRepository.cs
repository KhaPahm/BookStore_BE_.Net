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
        private readonly ICloudinaryService _cloudinaryService;

        public BookImageRepository(ApplicationDBContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<BookImage>> CreateAsync(Guid bookId, ICollection<IFormFile> images)
        {
            List<BookImage> lstBookImage = new();

            foreach(var image in images) {
                var bookImage = new BookImage {
                    BookId = bookId
                };

                var url = await _cloudinaryService.UploadImageAsync(image);
                bookImage.ImageUrl = url;
                await _context.BookImages.AddAsync(bookImage);
                lstBookImage.Add(bookImage);
                _context.SaveChanges();
            } 

            return lstBookImage;
        }
    }
}