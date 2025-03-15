using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Book;
using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly IPublisherRepository _publisherRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IBookImageRepository _bookImageRepo;

        public BookController(IBookRepository bookRepo, IPublisherRepository publisherRepo, 
        ICategoryRepository categoryRepo, IBookImageRepository bookImageRepo)
        {
            _bookRepo = bookRepo;
            _publisherRepo = publisherRepo;
            _categoryRepo = categoryRepo;
            _bookImageRepo = bookImageRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var books = await _bookRepo.GetAllAsync();
            var booksDto = books.Select(b => b.ToBookDto()).ToList();
            return Ok(booksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            var book = await _bookRepo.GetByIdAsync(id);
            var bookDto = book.ToBookDto();
            return Ok(bookDto);            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBookDto bookDto) {
            var bookModel = bookDto.ToBookFromCreateBookDto();
            await _bookRepo.CreateAsync(bookModel);

            var publisher = await _publisherRepo.GetByIdAsync(bookModel.PublisherId);
            var category = await _categoryRepo.GetByIdAsync(bookModel.CategoryId);
            var lstBookImage = await _bookImageRepo.CreateAsync(bookModel.Id, bookDto.Images);

            bookModel.Publisher = publisher;
            bookModel.Category = category;
            bookModel.Images = lstBookImage;
             
            return CreatedAtAction(nameof(GetById), new {id = bookModel.Id}, bookModel.ToBookDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateBookDto bookDto) {
            var bookModel = bookDto.ToBookFromUpdateBookDto();

            var newBookUpdate = await _bookRepo.UpdateAsync(id, bookModel);
            if(newBookUpdate == null)
                return NotFound("Coudn't find book!");

            return Ok(newBookUpdate.ToBookDto());
        }
    }
}