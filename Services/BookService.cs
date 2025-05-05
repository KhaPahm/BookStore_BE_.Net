using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Controllers.V1;
using BookStore.Dtos.Book;
using BookStore.Exceptions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherService _publisherService;
        private readonly ICategoryService _categoryService;
        private readonly IBookImageService _bookImageService;

        public BookService(IBookRepository bookRepository, IPublisherService publisherService, ICategoryService categoryService, IBookImageService bookImageService)
        {
            _bookRepository = bookRepository;
            _publisherService = publisherService;
            _categoryService = categoryService;
            _bookImageService = bookImageService;
        }
        public async Task<BookDto> CreateBookDtoAsync(CreateBookDto creatBookDto)
        {
            var publisherDto = await _publisherService.GetPublisherDtoByIdAsync(creatBookDto.PublisherId);
            
            var categoryDto = await _categoryService.GetCategoryDtoByIdAsync(creatBookDto.CategoryId);
            
            var bookModel = creatBookDto.ToBookModel();
            await _bookRepository.CreateAsync(bookModel);

            var lstBookImage = await _bookImageService.CreateBookImagesAsync(bookModel.Id, creatBookDto.Images);
            var bookDto = bookModel.ToBookDto();
            
            bookDto.Publisher = publisherDto;
            bookDto.Category = categoryDto;
            bookDto.Images = lstBookImage;
            return bookDto;
        }

        public async Task<List<BookDto>> GetAllBookDtosAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var booksDtos = books.Select(b => b.ToBookDto()).ToList();
            return booksDtos;
        }

        public Task<BookDto> GetBookDtoByIdAsync(Guid id)
        {
            var book = _bookRepository.GetByIdAsync(id).Result;
            if (book == null) 
                throw new NotFoundException($"Book is not found.");
            var bookDto = book.ToBookDto();
            return Task.FromResult(bookDto);
        }

        public async Task<bool> IsBookExistAsync(Guid id)
        {
            return await _bookRepository.IsBookExistAsync(id);
        }

        public async Task<BookDto> UpdateBookDtoAsync(Guid id, UpdateBookDto bookDto)
        {
            var bookModel = bookDto.ToBookModel();
            var isPublisherExist = await _publisherService.IsPublisherExist(bookDto.PublisherId);
            if (!isPublisherExist) 
                throw new NotFoundException($"Publisher is not found.");

            var isCategoryExist = await _categoryService.IsCategoryExistAsync(bookModel.CategoryId);
            if (!isCategoryExist) 
                throw new NotFoundException($"Category is not found.");

            var newBookUpdate = await _bookRepository.UpdateAsync(id, bookModel);
            if (newBookUpdate == null) 
                throw new NotFoundException($"Book is not found.");

            return newBookUpdate.ToBookDto();
        }
    }
}