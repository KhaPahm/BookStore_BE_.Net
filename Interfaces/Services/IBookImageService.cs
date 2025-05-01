using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.BookImage;

namespace BookStore.Interfaces.Services
{
    public interface IBookImageService
    {
        public Task<List<BookImageDto>> CreateBookImagesAsync(Guid bookId, ICollection<IFormFile> images);
    }
}