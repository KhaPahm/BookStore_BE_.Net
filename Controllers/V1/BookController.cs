using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Book;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappings;
using BookStore.Models;
using BookStore.Models.ResponeApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var booksDtos = await _bookService.GetAllBookDtosAsync();
            return Ok(new ApiResponse<List<BookDto>>(200, booksDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            var bookDto = await _bookService.GetBookDtoByIdAsync(id);
            return Ok(new ApiResponse<BookDto>(200, bookDto));            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBookDto createBookDto) {
            var bookDto = await _bookService.CreateBookDtoAsync(createBookDto);
            return CreatedAtAction(nameof(GetById), new {id = bookDto}, new ApiResponse<BookDto>(201, bookDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateBookDto bookDto) {
            var book = await _bookService.UpdateBookDtoAsync(id, bookDto);
            return Ok(new ApiResponse<BookDto>(200, book));
        }
    }
}