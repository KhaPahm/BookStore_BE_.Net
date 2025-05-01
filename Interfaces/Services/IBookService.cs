using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Controllers.V1;
using BookStore.Dtos.Book;

namespace BookStore.Interfaces.Services
{
    public interface IBookService
    {
        public Task<List<BookDto>> GetAllBookDtosAsync();
        public Task<BookDto> GetBookDtoByIdAsync(Guid id);
        public Task<BookDto> CreateBookDtoAsync(CreateBookDto bookDto);
        public Task<BookDto> UpdateBookDtoAsync(Guid id, UpdateBookDto bookDto);
    }
}