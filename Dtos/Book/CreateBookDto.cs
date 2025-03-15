using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<IFormFile>? Images { get; set; } // Accept image files
    }
}