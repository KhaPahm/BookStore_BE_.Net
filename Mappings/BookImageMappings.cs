using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.BookImage;
using BookStore.Models;

namespace BookStore.Mappings
{
    public static class BookImageMappings
    {
        public static BookImageDto ToBookImageDto(this BookImage bookImage) {
            return new BookImageDto {
                Id = bookImage.Id,
                ImageUrl = bookImage.ImageUrl
            };
        }
    }
}