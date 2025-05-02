using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Book;
using BookStore.Models;

namespace BookStore.Mappers
{
    public static class BookMappers
    {
        public static Book ToBookModel(this UpdateBookDto updateBookDto) {
            return new Book {
                Title = updateBookDto.Title,
                Author = updateBookDto.Author,
                Description = updateBookDto.Description,
                Price = updateBookDto.Price,
                CategoryId = updateBookDto.CategoryId,
                PublisherId = updateBookDto.PublisherId,
                StockQuantity = updateBookDto.StockQuantity,
                PublishedDate = updateBookDto.PublishedDate
            };
        }

        public static Book ToBookModel(this CreateBookDto createBookDto) {
            return new Book {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                Description = createBookDto.Description,
                Price = createBookDto.Price,
                CategoryId = createBookDto.CategoryId,
                PublisherId = createBookDto.PublisherId,
                StockQuantity = createBookDto.StockQuantity,
                PublishedDate = createBookDto.PublishedDate
            };
        }

        public static BookDto ToBookDto(this Book book) {
            return new BookDto {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                PublishedDate = book.PublishedDate,
                CreateAt = book.CreateAt,
                Category = book.Category == null ? null : book.Category.ToCategoryDto(),
                Publisher = book.Publisher == null ? null : book.Publisher.ToPublisherDto(),
                Images = book.Images == null ? null : book.Images.Select(image => image.ToBookImageDto()).ToList()
            };
        }
    }
}