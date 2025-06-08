using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.BookImage;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappings;
using BookStore.Models;

namespace BookStore.Services
{
    public class BookImageService : IBookImageService
    {
        private readonly IBookImageRepository _bookImageRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public BookImageService(IBookImageRepository bookImageRepository, ICloudinaryService cloudinaryService)
        {
            _bookImageRepository = bookImageRepository;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<List<BookImageDto>> CreateBookImagesAsync(Guid bookId, ICollection<IFormFile> images)
        {
            List<BookImage> lstBookImage = new();

            foreach(var image in images) {
                var bookImage = new BookImage {
                    BookId = bookId
                };
                var url = await _cloudinaryService.UploadImageAsync(image);
                bookImage.ImageUrl = url;
                await _bookImageRepository.CreateAsync(bookImage);
                lstBookImage.Add(bookImage);
            } 

            return lstBookImage.Select(bookImage => bookImage.ToBookImageDto()).ToList();
        }
    }
}