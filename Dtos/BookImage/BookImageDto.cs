using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.BookImage
{
    public class BookImageDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
    }
}